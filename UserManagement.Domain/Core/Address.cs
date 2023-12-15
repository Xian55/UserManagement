namespace UserManagement.Domain.Core;

/// <summary>
/// Represents an address.
/// </summary>
public class Address
{
    public required string Street { get; set; }
    public required string Suite { get; set; }
    public required string City { get; set; }
    public required string Zipcode { get; set; }
    public required Geo Geo { get; set; }
}
