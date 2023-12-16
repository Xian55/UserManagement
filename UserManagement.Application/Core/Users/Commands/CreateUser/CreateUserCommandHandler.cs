using System.Threading;
using System.Threading.Tasks;

using UserManagement.Application.Abstractions.Data;
using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Application.Validation;
using UserManagement.Domain.Core;
using UserManagement.Domain.Core.Errors;
using UserManagement.Domain.Primitives.Maybe;
using UserManagement.Domain.Primitives.Result;

namespace UserManagement.Application.Core.Users.Commands.CreateUser;

/// <summary>
/// Represents the <see cref="CreateUserCommand"/> handler.
/// </summary>
internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public CreateUserCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool userNameAlreadyTaken = _dbContext.Set<User>().Where(x => x.Username == request.Username).Any();
        if (userNameAlreadyTaken)
        {
            return Result.Failure(DomainErrors.User.UserNameMustBeUnique);
        }

        bool emailAlreadyTaken = _dbContext.Set<User>().Where(x => x.Email == request.Email).Any();
        if (emailAlreadyTaken)
        {
            return Result.Failure(DomainErrors.User.EmailMustBeUnique);
        }

        var user = new User
        {
            Address = request.Address,
            Company = request.Company,
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            Username = request.Username,
            Website = request.Website
        };

        _dbContext.Insert(user);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
