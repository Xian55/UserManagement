namespace UserMangement.Application.UnitTests.Users.Commands
{
    public sealed class CreateUserCommandHandlerTests
    {
        private readonly Mock<IDbContext> _dbContext;

        private readonly IList<User> _users = new List<User>()
        {
            new()
            {
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

        public CreateUserCommandHandlerTests()
        {
            _dbContext = new Mock<IDbContext>();
            _dbContext.Setup(x => x.Set<User>()).ReturnsDbSet(_users);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureResult_When_EmailIsNotUnique()
        {
            // Arrange
            var command = new CreateUserCommand()
            {
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

            var handler = new CreateUserCommandHandler(_dbContext.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DomainErrors.User.EmailMustBeUnique);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureResult_When_UsernameIsNotUnique()
        {
            // Arrange
            var command = new CreateUserCommand()
            {
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

            var handler = new CreateUserCommandHandler(_dbContext.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DomainErrors.User.UserNameMustBeUnique);
        }


        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_When_UserIsCreated()
        {
            // Arrange
            var command = new CreateUserCommand()
            {
                Email = "test_created@test.com",
                Name = "test",
                Phone = "test",
                Username = "test_created",
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

            var handler = new CreateUserCommandHandler(_dbContext.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}