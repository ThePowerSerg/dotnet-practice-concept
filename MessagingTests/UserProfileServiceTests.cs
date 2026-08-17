using MessagingAPI.Models;
using Moq;
using ReviewAPI.Repositories;
using ReviewAPI.Services;

namespace MessagingTests;

public class UserProfileServiceTests
{
    [Fact]
    public async Task GetUserProfileByIdAsync_returns_profile_from_repository()
    {
        // Arrange: create a mock repository and define the result it should return
        // when the service requests the profile with ID 1.
        var repository = new Mock<IUserProfileRepository>();
        repository
            .Setup(repo => repo.GetUserProfileByIdAsync(1))
            .ReturnsAsync(new UserProfile { Id = 1, UserName = "Ada" });

        var service = new UserProfileService(repository.Object);

        // Act: call the service method being tested.
        var result = await service.GetUserProfileByIdAsync(1);

        // Assert: verify that the service returns the repository data as a DTO.
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Ada", result.UserName);
    }
}
