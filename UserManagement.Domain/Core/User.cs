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


    public Result UpdateInformation(
        string? name, string? userName, string? email, Address? address,
        string? phone, string? website, Company? company)
    {
        // TODO: validate
        if (name is not null)
        {
            Name = name;
        }

        if (userName is not null)
        {
            Username = userName;
        }

        if (email is not null)
        {
            Email = email;
        }

        if (address is not null)
        {
            Address = address;
        }

        if (phone is not null)
        {
            Phone = phone;
        }

        if (website is not null)
        {
            Website = website;
        }

        if (company is not null)
        {
            Company = company;
        }

        return Result.Success();
    }
}
