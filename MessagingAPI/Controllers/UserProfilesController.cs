using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewAPI.Dtos;
using ReviewAPI.Services;

namespace ReviewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfilesController(IUserProfileService profileService) : ControllerBase
    {
        // private readonly IUserProfileService profileService;

        // public UserProfilesController(IUserProfileService profileService)
        // {
        //     this.profileService = profileService;
        // }

        // Get request 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> Get()
        {
            var userProfiles = await profileService.GetUserProfilesAsync();
            return Ok(userProfiles);
        }

        [HttpGet("{id}")]
        // Get request by Id 
        public async Task<ActionResult<UserProfileDto>> GetById(int id)
        {
            var userProfile = await profileService.GetUserProfileByIdAsync(id);

            if (userProfile == null) return NotFound();

            return Ok(userProfile);
        }

        [HttpPost]
        public async Task<ActionResult<UserProfileDto>> Create([FromBody] CreateUserProfileDto userProfile)
        {
            var profile = await profileService.CreateUserProfileAsync(userProfile);

            return CreatedAtAction(
                nameof(GetById),
                new { id = profile.Id },
                profile);
        }
    }
}