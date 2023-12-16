using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Domain.Core;
using UserManagement.Domain.Primitives.Result;

namespace UserManagement.Application.Core.Users.Commands.CreateUser;

public sealed class CreateUserCommand : ICommand<Result>
{
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required Address Address { get; init; }
    public required string Phone { get; init; }
    public required string Website { get; init; }
    public required Company Company { get; init; }
}
