using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Application.Contracts.Users;
using UserManagement.Domain.Primitives.Maybe;

namespace UserManagement.Application.Core.Users.Queries.GetUserById;

/// <summary>
/// Represents the query for getting the user by identifier.
/// </summary>
public sealed class GetUserByIdQuery : IQuery<Maybe<UserResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQuery"/> class.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    public GetUserByIdQuery(int userId) => UserId = userId;

    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    public int UserId { get; }
}
