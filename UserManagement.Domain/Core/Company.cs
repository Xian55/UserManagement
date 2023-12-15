namespace UserManagement.Domain.Core;

/// <summary>
/// Represents a company.
/// </summary>
public sealed class Company
{
    public required string Name { get; set; }
    public required string CatchPhrase { get; set; }
    public required string Bs { get; set; }
}
