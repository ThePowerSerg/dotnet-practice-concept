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
        public async Task<ActionResult<UserProfileDto>> Post([FromBody] CreateUserProfileDto userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = profileService.CreateUserProfileAsync(userProfile);

            return CreatedAtAction(
                nameof(GetById), 
                new {id = profile.Id }, 
                profile);
        }
    }
}