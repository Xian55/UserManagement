using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Domain.Core;
using UserManagement.Domain.Primitives.Result;

namespace UserManagement.Application.Core.Users.Commands.UpdateUser;

/// <summary>
/// Represents the command for updating user information.
/// </summary>
public sealed class UpdateUserCommand : ICommand<Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserCommand"/> class.
    /// </summary>
    /// 
    public required string UserId { get; init; }

    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required Address Address { get; init; }
    public required string Phone { get; init; }
    public required string Website { get; init; }
    public required Company Company { get; init; }
}
