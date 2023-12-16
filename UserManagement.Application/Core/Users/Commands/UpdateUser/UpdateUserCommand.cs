using System;
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
    public int UserId { get; init; }

    public string? Name { get; init; }
    public string? Username { get; init; }
    public string? Email { get; init; }
    public Address? Address { get; init; }
    public string? Phone { get; init; }
    public string? Website { get; init; }
    public Company? Company { get; init; }
}
