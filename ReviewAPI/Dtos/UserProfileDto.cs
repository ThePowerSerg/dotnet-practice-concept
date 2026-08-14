/// <summary>
/// Represents the user-profile data returned by this API.
/// </summary>
/// <remarks>
/// This DTO is the API's response contract. Keeping it separate from the
/// <c>UserProfile</c> EF Core entity prevents database-model changes from
/// unintentionally changing the data exposed by API endpoints.
/// </remarks>

namespace ReviewAPI.Dtos
{
    public class UserProfileDto
    {
        public int Id { get; init; }
        public required string UserName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Country { get; init; }
    }
}