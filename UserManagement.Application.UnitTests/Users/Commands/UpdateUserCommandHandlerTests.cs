using UserManagement.Application.Core.Users.Commands.UpdateUser;

namespace UserManagement.Application.UnitTests.Users.Commands;

public sealed class UpdateUserCommandHandlerTests
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

    public UpdateUserCommandHandlerTests()
    {
        _dbContext = new Mock<IDbContext>();
        _dbContext
            .Setup(x => x.Set<User>())
            .ReturnsDbSet(_users);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_When_UserIdNotFound()
    {
        // Arrange
        var command = new UpdateUserCommand()
        {
            UserId = "invalid_or_non_existing_id",
            Email = "email@test.com",
            Name = "test",
            Phone = "test",
            Username = "test_email_not_unique",
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
        };

        _dbContext
            .Setup(x => x.GetBydIdAsync<User>(command.UserId))
            .ReturnsAsync(Maybe<User>.None);

        var handler = new UpdateUserCommandHandler(_dbContext.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ValidationErrors.User.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_When_UsernameIsNotUnique()
    {
        // Arrange
        var command = new UpdateUserCommand()
        {
            UserId = "1",
            Email = "unique@test.com",
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
        };

        _dbContext
            .Setup(x => x.GetBydIdAsync<User>(command.UserId))
            .ReturnsAsync(Maybe<User>.From(_users.First()));

        var handler = new UpdateUserCommandHandler(_dbContext.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UserNameMustBeUnique);
    }


    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_When_UserIsUpdated()
    {
        // Arrange
        var command = new UpdateUserCommand()
        {
            UserId = "1",
            Email = "test_updated@test.com",
            Name = "test",
            Phone = "test",
            Username = "test_updated",
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
        };

        _dbContext
            .Setup(x => x.GetBydIdAsync<User>(command.UserId))
            .ReturnsAsync(Maybe<User>.From(_users.First()));

        var handler = new UpdateUserCommandHandler(_dbContext.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}