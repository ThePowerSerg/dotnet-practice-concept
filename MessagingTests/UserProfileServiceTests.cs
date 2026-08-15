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
        var repository = new Mock<IUserProfileRepository>();
        repository
            .Setup(repo => repo.GetUserProfileByIdAsync(1))
            .ReturnsAsync(new UserProfile { Id = 1, UserName = "Ada" });

        var service = new UserProfileService(repository.Object);

        var result = await service.GetUserProfileByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Ada", result.UserName);
    }
}
