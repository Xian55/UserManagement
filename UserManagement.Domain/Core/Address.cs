using UserManagement.Domain.Primitives;

namespace UserManagement.Domain.Core;

/// <summary>
/// Represents an address.
/// </summary>
public sealed class Address : ValueObject, IEquatable<Address>
{
    public required string Street { get; set; }
    public required string Suite { get; set; }
    public required string City { get; set; }
    public required string Zipcode { get; set; }
    public required Geo Geo { get; set; }

    public bool Equals(Address? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return
            Street == other.Street &&
            Suite == other.Suite &&
            City == other.City &&
            Zipcode == other.Zipcode &&
            Geo.Equals(other.Geo);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Street;
        yield return Suite;
        yield return City;
        yield return Zipcode;
        yield return Geo;
    }
}
