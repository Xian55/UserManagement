using UserManagement.Application.Contracts.Users;
using UserManagement.Application.Core.Users.Queries.GetUserById;

namespace UserManagement.Application.UnitTests.Users.Queries;

public sealed class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IDbContext> _dbContext;

    private readonly IList<User> _users = new List<User>()
    {
        new()
        {
            Id = "1",
            Email = "email@test.com",
            Name = "test",
            Phone = "test",
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

    public GetUserByIdQueryHandlerTests()
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
        var query = new GetUserByIdQuery("not_existing_id");

        var handler = new GetUserByIdQueryHandler(_dbContext.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().Be(Maybe<UserResponse>.None);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_When_UserExists()
    {
        // Arrange
        var query = new GetUserByIdQuery("1");

        var handler = new GetUserByIdQueryHandler(_dbContext.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().NotBe(Maybe<UserResponse>.None);
    }
}