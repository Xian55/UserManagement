using UserManagement.Domain.Core;

namespace UserManagement.Application.Contracts.Users;

/// <summary>
/// Represents the update user request.
/// </summary>
public sealed class UpdateUserRequest
{
    public string? Name { get; init; }
    public string? Username { get; init; }
    public string? Email { get; init; }
    public Address? Address { get; init; }
    public string? Phone { get; init; }
    public string? Website { get; init; }
    public Company? Company { get; init; }
}
