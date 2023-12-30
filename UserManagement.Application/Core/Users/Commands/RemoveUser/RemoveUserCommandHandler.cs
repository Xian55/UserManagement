using UserManagement.Application.Abstractions.Data;
using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Application.Validation;
using UserManagement.Domain.Core;
using UserManagement.Domain.Primitives.Maybe;
using UserManagement.Domain.Primitives.Result;

namespace UserManagement.Application.Core.Users.Commands.RemoveUser;

/// <summary>
/// Represents the <see cref="RemoveUserCommand"/> handler.
/// </summary>
internal sealed class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand, Result>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveUserCommandHandler"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public RemoveUserCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<Result> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await _dbContext.GetBydIdAsync<User>(request.UserId);
        if (maybeUser.HasNoValue)
        {
            return Result.Failure(ValidationErrors.User.NotFound);
        }

        User user = maybeUser.Value;

        _dbContext.Remove(user);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
