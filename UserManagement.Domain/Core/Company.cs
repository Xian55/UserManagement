using UserManagement.Domain.Primitives;

namespace UserManagement.Domain.Core;

/// <summary>
/// Represents a company.
/// </summary>
public sealed class Company : ValueObject, IEquatable<Company>
{
    public required string Name { get; set; }
    public required string CatchPhrase { get; set; }
    public required string Bs { get; set; }

    public bool Equals(Company? other)
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
            Name == other.Name &&
            CatchPhrase == other.CatchPhrase &&
            Bs == other.Bs;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return CatchPhrase;
        yield return Bs;
    }
}
