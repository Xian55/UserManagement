using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Domain.Primitives.Result;

namespace UserManagement.Application.Core.Users.Commands.RemoveUser;

/// <summary>
/// Represents the command for removing a user.
/// </summary>
public sealed class RemoveUserCommand : ICommand<Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveUserCommand"/> class.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    public RemoveUserCommand(int userId) => UserId = userId;

    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    public int UserId { get; }
}
