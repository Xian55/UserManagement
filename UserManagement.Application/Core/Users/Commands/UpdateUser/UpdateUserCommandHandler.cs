using UserManagement.Application.Abstractions.Data;
using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Application.Validation;
using UserManagement.Domain.Core;
using UserManagement.Domain.Core.Errors;
using UserManagement.Domain.Primitives.Maybe;
using UserManagement.Domain.Primitives.Result;

namespace UserManagement.Application.Core.Users.Commands.UpdateUser;

/// <summary>
/// Represents the <see cref="UpdateUserCommand"/> handler.
/// </summary>
internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UpdateUserCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await _dbContext.GetBydIdAsync<User>(request.UserId);
        if (maybeUser.HasNoValue)
        {
            return Result.Failure(ValidationErrors.User.NotFound);
        }

        User user = maybeUser.Value;

        Result updateInformationResult = user.UpdateInformation(
            request.Name, request.Username, request.Email, request.Address,
            request.Phone, request.Website, request.Company);

        if (updateInformationResult.IsFailure)
        {
            return Result.Failure(updateInformationResult.Error);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
