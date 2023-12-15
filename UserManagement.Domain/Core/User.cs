namespace UserManagement.Domain.Core;

/// <summary>
/// Represents a user.
/// </summary>
public sealed class User
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required Address Address { get; set; }
    public required string Phone { get; set; }
    public required string Website { get; set; }
    public required Company Company { get; set; }
}
