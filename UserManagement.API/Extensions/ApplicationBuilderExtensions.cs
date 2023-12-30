using System.Text.Json;

using UserManagement.Api.Middleware;
using UserManagement.Persistence;
using UserManagement.Persistence.Extensions;

namespace UserManagement.API.Extensions;

internal static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configure the custom exception handler middleware.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The same application builder.</returns>
    internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<CustomExceptionHandlerMiddleware>();

    /// <summary>
    /// Configures the Swagger and SwaggerUI middleware.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The same application builder.</returns>
    internal static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder builder)
    {
        builder.UseSwagger();

        builder.UseSwaggerUI(swaggerUiOptions =>
            swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API"));

        return builder;
    }

    internal static IApplicationBuilder EnsureDatabaseCreated(this IApplicationBuilder builder)
    {
        using IServiceScope serviceScope = builder.ApplicationServices.CreateScope();

        using UserMangementDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<UserMangementDbContext>();

        //dbContext.Database.EnsureCreated();

        JsonSerializerOptions options = serviceScope.ServiceProvider.GetRequiredService<JsonSerializerOptions>();

        dbContext.SeedDatabase(options);

        return builder;
    }
}
