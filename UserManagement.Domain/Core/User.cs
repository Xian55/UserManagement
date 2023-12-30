using UserManagement.Domain.Primitives;
using UserManagement.Domain.Primitives.Result;

namespace UserManagement.Domain.Core;

/// <summary>
/// Represents a user.
/// </summary>
public sealed class User : Entity
{
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required Address Address { get; set; }
    public required string Phone { get; set; }
    public required string Website { get; set; }
    public required Company Company { get; set; }


    public Result Update(
        string name, string userName, string email, Address address,
        string phone, string website, Company company)
    {
        Name = name;
        Username = userName;
        Email = email;
        Address = address;
        Phone = phone;
        Website = website;
        Company = company;

        return Result.Success();
    }
}
