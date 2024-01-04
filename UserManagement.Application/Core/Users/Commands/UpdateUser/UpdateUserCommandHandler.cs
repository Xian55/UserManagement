using UserManagement.Application.Abstractions.Data;
using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Application.Validation;
using UserManagement.Domain.Core;
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

        bool userNameAlreadyTaken = _dbContext.Set<User>().Where(x => x.Username == request.Username).Any();
        if (userNameAlreadyTaken)
        {
            return Result.Failure(Domain.Core.Errors.DomainErrors.User.UserNameMustBeUnique);
        }

        bool emailAlreadyTaken = _dbContext.Set<User>().Where(x => x.Email == request.Email).Any();
        if (emailAlreadyTaken)
        {
            return Result.Failure(Domain.Core.Errors.DomainErrors.User.EmailMustBeUnique);
        }

        User user = maybeUser.Value;

        Result updatedResult = user.Update(
            request.Name, request.Username, request.Email, request.Address,
            request.Phone, request.Website, request.Company);

        if (updatedResult.IsFailure)
        {
            return Result.Failure(updatedResult.Error);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
