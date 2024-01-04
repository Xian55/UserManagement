using UserManagement.Application.Core.Users.Commands.RemoveUser;

namespace UserManagement.Application.UnitTests.Users.Commands;

public sealed class RemoveUserCommandHandlerTests
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
            Company = new()
            { 
                Bs = "test",
                CatchPhrase = "test",
                Name = "test"
            },
        }
    };

    public RemoveUserCommandHandlerTests()
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
        var command = new RemoveUserCommand("invalid_or_non_existing_id");

        _dbContext
            .Setup(x => x.GetBydIdAsync<User>(command.UserId))
            .ReturnsAsync(Maybe<User>.None);

        var handler = new RemoveUserCommandHandler(_dbContext.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ValidationErrors.User.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_When_UserFound()
    {
        // Arrange
        var command = new RemoveUserCommand("1");

        _dbContext
            .Setup(x => x.GetBydIdAsync<User>(command.UserId))
            .ReturnsAsync(Maybe<User>.From(_users.First()));

        var handler = new RemoveUserCommandHandler(_dbContext.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
