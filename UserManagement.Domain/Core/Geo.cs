using UserManagement.Domain.Primitives;

namespace UserManagement.Domain.Core;

/// <summary>
/// Represents a geo location.
/// </summary>
public sealed class Geo : ValueObject, IEquatable<Geo>
{
    public required string Lat { get; set; }
    public required string Lng { get; set; }

    public bool Equals(Geo? other)
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
            Lat == other.Lat &&
            Lng == other.Lng;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Lat;
        yield return Lng;
    }
}
