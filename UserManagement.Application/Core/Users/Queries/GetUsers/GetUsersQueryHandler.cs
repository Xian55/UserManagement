
using System.Linq.Dynamic.Core;

using Microsoft.EntityFrameworkCore;

using UserManagement.Application.Abstractions.Data;
using UserManagement.Application.Abstractions.Messaging;
using UserManagement.Application.Contracts.Common;
using UserManagement.Application.Contracts.Users;
using UserManagement.Domain.Core;

namespace UserManagement.Application.Core.Users.Queries.GetUsers;

/// <summary>
/// Represents the <see cref="GetUsersQuery"/> handler.
/// </summary>
internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagedList<UserResponse>>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersQueryHandler"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public GetUsersQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<PagedList<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<UserResponse> usersQuery = _dbContext.Set<User>()
            .AsNoTracking()
            .Where(user =>
                (string.IsNullOrEmpty(request.Name) || user.Name.ToLower().Contains(request.Name)) &&
                (string.IsNullOrEmpty(request.Username) || user.Username.ToLower().Contains(request.Username)) &&
                (string.IsNullOrEmpty(request.Email) || user.Email.ToLower().Contains(request.Email)) &&
                (string.IsNullOrEmpty(request.Phone) || user.Phone.ToLower().Contains(request.Phone)) &&
                (string.IsNullOrEmpty(request.Website) || user.Website.ToLower().Contains(request.Website)) &&

                (request.Address == null ||
                    (string.IsNullOrEmpty(request.Address.City) || user.Address.City.ToLower().Contains(request.Address.City)) &&
                    (string.IsNullOrEmpty(request.Address.Street) || user.Address.Street.ToLower().Contains(request.Address.Street)) &&
                    (string.IsNullOrEmpty(request.Address.Suite) || user.Address.Suite.ToLower().Contains(request.Address.Suite)) &&
                    (string.IsNullOrEmpty(request.Address.Zipcode) || user.Address.Zipcode.ToLower().Contains(request.Address.Zipcode))) &&

                (request.Company == null ||
                    (string.IsNullOrEmpty(request.Company.Name) || user.Company.Name.ToLower().Contains(request.Company.Name)))
                )
            .OrderBy(request.OrderBy)
            .Select(user => new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Website = user.Website,
                Company = user.Company
            });

        List<UserResponse> users = await usersQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        int totalCount = await usersQuery.CountAsync(cancellationToken);

        var response = new PagedList<UserResponse>(users, totalCount, request.Page, request.PageSize);

        return response;
    }
}
