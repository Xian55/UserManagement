namespace UserManagement.Domain.Core;

/// <summary>
/// Represents a geo location.
/// </summary>
public sealed class Geo
{
    public required string Lat { get; set; }
    public required string Lng { get; set; }
}
