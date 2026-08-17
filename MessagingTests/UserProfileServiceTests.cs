using MessagingAPI.Models;
using Moq;
using ReviewAPI.Repositories;
using ReviewAPI.Services;

namespace MessagingTests
{
    public class UserProfileServiceTests
    {
        private readonly Mock<IUserProfileRepository> repository;
        private readonly UserProfileService service;

        public UserProfileServiceTests()
        {
            // create a mock repository 
            repository = new Mock<IUserProfileRepository>();
            service = new UserProfileService(repository.Object);
        }

        [Fact]
        public async Task GetUserProfileByIdAsync()
        {
            // Arrange: Define the repository call the mock should recognize and what sholud be 
            // returned when the service requests the UserProfile 1
            repository.Setup(repo => repo.GetUserProfileByIdAsync(1))
                      .ReturnsAsync(new UserProfile { Id = 1, UserName = "Ada" });

            // Act
            var result = await service.GetUserProfileByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Ada", result.UserName);
        }
    }
}