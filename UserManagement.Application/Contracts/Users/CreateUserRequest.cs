using UserManagement.Domain.Core;

namespace UserManagement.Application.Contracts.Users;

public sealed class CreateUserRequest
{
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required Address Address { get; init; }
    public required string Phone { get; init; }
    public required string Website { get; init; }
    public required Company Company { get; init; }
}
