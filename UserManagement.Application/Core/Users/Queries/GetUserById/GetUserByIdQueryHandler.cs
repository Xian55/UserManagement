using Microsoft.EntityFrameworkCore;

using UserManagement.Application.Abstractions.Data;
using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Application.Contracts.Users;
using UserManagement.Domain.Core;
using UserManagement.Domain.Primitives.Maybe;

namespace UserManagement.Application.Core.Users.Queries.GetUserById;

/// <summary>
/// Represents the <see cref="GetUserByIdQuery"/> handler.
/// </summary>
internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Maybe<UserResponse>>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public GetUserByIdQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<Maybe<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.UserId))
        {
            return Maybe<UserResponse>.None;
        }

        Maybe<User> maybeUser =
            await _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Maybe<UserResponse>.None;
        }

        User user = maybeUser.Value;

        var response = new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            Website = user.Website,
            Company = user.Company
        };

        return response;
    }
}
