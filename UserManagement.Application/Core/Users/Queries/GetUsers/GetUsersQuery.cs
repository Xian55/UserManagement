using System.Text;

using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Application.Contracts.Common;
using UserManagement.Application.Contracts.Users;
using UserManagement.Domain.Core;

namespace UserManagement.Application.Core.Users.Queries.GetUsers;

/// <summary>
/// Represents the query for getting a collection of users.
/// </summary>
public sealed class GetUsersQuery : IQuery<PagedList<UserResponse>>
{
    private const int MaxPageSize = 50;

    private const string DefaultOrderBy = $"{nameof(User.Name)}";

    private static readonly Dictionary<string, string> ValidOrderByColumnsDictionary = new()
    {
        { nameof(User.Company).ToLower(), $"{nameof(User.Company)}.{nameof(User.Company.Name)}" },
        { nameof(User.Address).ToLower(), $"{nameof(User.Address)}.{nameof(User.Address.Zipcode)}" }
    };

    public GetUsersQuery(
        string? name,
        string? username,
        string? email,
        Address? address,
        string? phone,
        string? website,
        Company? company,
        int page,
        int pageSize,
        string orderBy)
    {
        Name = name;
        Username = username;
        Email = email;
        Address = address;
        Phone = phone;
        Website = website;
        Company = company;

        Page = page < 0 ? 1 : page;
        PageSize = pageSize < 0 ? 0 : pageSize > MaxPageSize ? MaxPageSize : pageSize;
        OrderBy = ValidateOrderBy(orderBy);
    }

    public string? Name { get; }
    public string? Username { get; }
    public string? Email { get; }
    public Address? Address { get; }
    public string? Phone { get; }
    public string? Website { get; }
    public Company? Company { get; }

    /// <summary>
    /// Gets the page.
    /// </summary>
    public int Page { get; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Gets or sets the order by.
    /// </summary>
    public string OrderBy { get; set; }

    /// <summary>
    /// Validates the provided order by string.
    /// </summary>
    /// <param name="orderBy">The order by string.</param>
    /// <returns>The validated order by string or a default order by.</returns>
    private static string ValidateOrderBy(string orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            return DefaultOrderBy;
        }

        string[] orderByParts = orderBy.Split(',', StringSplitOptions.RemoveEmptyEntries);

        StringBuilder orderByStringBuilder = new();

        foreach (string orderByPart in orderByParts)
        {
            string columnName = orderByPart.Trim(' ').Split(' ')[0].ToLower();

            if (!ValidOrderByColumnsDictionary.TryGetValue(columnName, out string? orderByColumn))
            {
                continue;
            }

            string orderByDirection = orderByPart.EndsWith(" desc", StringComparison.OrdinalIgnoreCase) ? " desc" : string.Empty;
            string orderByValue = $"{orderByColumn}{orderByDirection}, ";
            orderByStringBuilder.Append(orderByValue);
        }

        string validatedOrderBy = orderByStringBuilder.ToString().TrimEnd(',', ' ');

        return string.IsNullOrEmpty(validatedOrderBy) ? DefaultOrderBy : validatedOrderBy;
    }
}
