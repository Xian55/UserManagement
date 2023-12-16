using System.Net;
using System.Text.Json;

using UserManagement.Api.Constants;
using UserManagement.Api.Contracts;
using UserManagement.Application.Exceptions;
using UserManagement.Domain.Primitives;

namespace UserManagement.Api.Middleware;

/// <summary>
/// Represents the exception handler middleware.
/// </summary>
internal sealed class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
    private readonly JsonSerializerOptions _options;

    private static readonly IReadOnlyCollection<Error> baseServerErrors = new[] { Errors.ServerError };

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomExceptionHandlerMiddleware"/> class.
    /// </summary>
    /// <param name="next">The delegate pointing to the next middleware in the chain.</param>
    /// <param name="logger">The logger.</param>
    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<CustomExceptionHandlerMiddleware> logger,
        JsonSerializerOptions options)
    {
        _next = next;
        _logger = logger;
        _options = options;
    }

    /// <summary>
    /// Invokes the exception handler middleware with the specified <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="httpContext">The HTTP httpContext.</param>
    /// <returns>The task that can be awaited by the next middleware.</returns>
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);

            await HandleExceptionAsync(httpContext, _options, ex);
        }
    }

    /// <summary>
    /// Handles the specified <see cref="Exception"/> for the specified <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="httpContext">The HTTP httpContext.</param>
    /// <param name="exception">The exception.</param>
    /// <returns>The HTTP response that is modified based on the exception.</returns>
    private static async Task HandleExceptionAsync(HttpContext httpContext, JsonSerializerOptions options, Exception exception)
    {
        (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error> errors) = GetHttpStatusCodeAndErrors(exception);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = (int)httpStatusCode;

        string response = JsonSerializer.Serialize(new ApiErrorResponse(errors), options);

        await httpContext.Response.WriteAsync(response);
    }

    /// <summary>
    /// Extracts the HTTP status code and a collection of errors based on the specified exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>The HTTP status code and a collection of errors based on the specified exception.</returns>
    private static (HttpStatusCode HttpStatusCode, IReadOnlyCollection<Error> Errors) GetHttpStatusCodeAndErrors(Exception exception) =>
        exception switch
        {
            ValidationException validationException =>
                (HttpStatusCode.BadRequest, validationException.Errors),

            _ =>
                (HttpStatusCode.InternalServerError, baseServerErrors)
        };
}
