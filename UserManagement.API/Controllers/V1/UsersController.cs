using MediatR;

using Microsoft.AspNetCore.Mvc;

using UserManagement.Api.Constants;
using UserManagement.Api.Contracts;
using UserManagement.Api.Infrastructure;
using UserManagement.Application.Contracts.Common;
using UserManagement.Application.Contracts.Users;
using UserManagement.Application.Core.Users.Commands.CreateUser;
using UserManagement.Application.Core.Users.Commands.RemoveUser;
using UserManagement.Application.Core.Users.Commands.UpdateUser;
using UserManagement.Application.Core.Users.Queries.GetUserById;
using UserManagement.Application.Core.Users.Queries.GetUsers;
using UserManagement.Domain.Core;
using UserManagement.Domain.Primitives.Maybe;
using UserManagement.Domain.Primitives.Result;

namespace UserManagement.API.Controllers.V1;

/// <summary>
/// Represents the users resource controller.
/// </summary>
[ApiVersion("1")]
[Route("api/v1/")]
public sealed class UsersController : ApiController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="sender">The sender.</param>
    public UsersController(ISender sender)
        : base(sender)
    {
    }

    /// <summary>
    /// Creates the user based on the specified request.
    /// </summary>
    /// <param name="request">The create user request.</param>
    /// <returns>200 - OK if the user was created successfully, otherwise 400 - Bad Request.</returns>
    [HttpPost(ApiRoutes.Users.CreateUser)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request) =>
        await Result.Create(request, Errors.UnProcessableRequest)
            .Map(value =>
                new CreateUserCommand
                {
                    Name = value.Name,
                    Username = value.Username,
                    Email = value.Email,
                    Address = value.Address,
                    Phone = value.Phone,
                    Website = value.Website,
                    Company = value.Company,
                })
            .Bind(command => Sender.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Updates the user with the specified identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="request">The update user request.</param>
    /// <returns>200 - OK if the user was updated successfully, otherwise 400 - Bad Request.</returns>
    [HttpPut(ApiRoutes.Users.UpdateUser)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserRequest request) =>
        await Result.Create(request, Errors.UnProcessableRequest)
            .Map(value =>
                new UpdateUserCommand
                {
                    UserId = userId,
                    Name = value.Name,
                    Username = value.Username,
                    Email = value.Email,
                    Address = value.Address,
                    Phone = value.Phone,
                    Website = value.Website,
                    Company = value.Company,
                })
            .Bind(command => Sender.Send(command))
            .Match(Ok, BadRequest);

    /// <summary>
    /// Removes the user with the specified identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>204 - No Content if the user was removed successfully, otherwise 400 - Bad Request.</returns>
    [HttpDelete(ApiRoutes.Users.RemoveUser)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveUser(string userId) =>
        await Result.Success(new RemoveUserCommand(userId))
            .Bind(command => Sender.Send(command))
            .Match(NoContent, BadRequest);

    /// <summary>
    /// Gets the user statistics for the user with the specified identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>200 - OK if the user with the specified identifier exists, otherwise 404 - Not Found.</returns>
    [HttpGet(ApiRoutes.Users.GetUserById)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(string userId) =>
        await Maybe<GetUserByIdQuery>.From(new GetUserByIdQuery(userId))
            .Bind(command => Sender.Send(command))
            .Match(Ok, NotFound);

    /// <summary>
    /// Gets the users for the specified parameters.
    /// </summary>
    /// <param name="name">by name.</param>
    /// <param name="username">by username.</param>
    /// <param name="email">by email.</param>
    /// <param name="address">by address.</param>
    /// <param name="phone">by phone.</param>
    /// <param name="website">by website</param>
    /// <param name="company">by company</param>
    /// <param name="page">The page.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="orderBy">The order by.</param>
    /// <returns>200 - OK response with the paged result of users for the specified parameters.</returns>
    [HttpGet(ApiRoutes.Users.GetUsers)]
    [ProducesResponseType(typeof(PagedList<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers(
        string? name,
        string? username,
        string? email,
        Address? address,
        string? phone,
        string? website,
        Company? company,
        int page,
        int pageSize,
        string orderBy) =>
        Ok(await Sender.Send(new GetUsersQuery(
            name,
            username,
            email,
            address,
            phone,
            website,
            company,
            page,
            pageSize,
            orderBy)));

}
