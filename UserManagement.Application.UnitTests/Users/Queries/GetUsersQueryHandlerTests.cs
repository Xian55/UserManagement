using UserManagement.Application.Contracts.Common;
using UserManagement.Application.Contracts.Users;
using UserManagement.Application.Core.Users.Queries.GetUsers;

namespace UserManagement.Application.UnitTests.Users.Queries;

public sealed class GetUsersQueryHandlerTests
{
    private readonly Mock<IDbContext> _dbContext;

    private readonly IList<User> _users = new List<User>()
    {
        new()
        {
            Id = "1",
            Email = "email@test.com",
            Name = "test",
            Phone = "1",
            Username = "test",
            Website = "www.test.com",
            Address = new()
            {
                City = "test",
                Street = "test",
                Suite = "test",
                Zipcode = "test",
                Geo = new() { Lat = "1", Lng = "1" }
            },
            Company = new() { Bs = "test", CatchPhrase = "test", Name = "test" },
        }
    };

    public GetUsersQueryHandlerTests()
    {
        _dbContext = new Mock<IDbContext>();
        _dbContext
            .Setup(x => x.Set<User>())
            .ReturnsDbSet(_users);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_When_UserNotFound()
    {
        // Arrange
        var query = new GetUsersQuery(
            "test2", "", "",
            new()
            {
                City = "",
                Street = "",
                Suite = "",
                Zipcode = "",
                Geo = new() { Lat = "", Lng = "" },
            },
            "", "",
            new() { Bs = "", CatchPhrase = "", Name = "" },
            1, 10, "");

        var handler = new GetUsersQueryHandler(_dbContext.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeEquivalentTo(PagedList<UserResponse>.Empty);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_When_UserExists()
    {
        // Arrange
        var query = new GetUsersQuery(
            "", "", "",
            new()
            {
                City = "",
                Street = "",
                Suite = "",
                Zipcode = "",
                Geo = new() { Lat = "", Lng = "" },
            },
            "1", "",
            new() { Bs = "", CatchPhrase = "", Name = "" },
            1, 10, "");

        var handler = new GetUsersQueryHandler(_dbContext.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().NotBe(PagedList<UserResponse>.Empty);
    }
}