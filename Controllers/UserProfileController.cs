using Auth.Dtos;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserService _userService;

        public UserProfileController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var userProfile = await _userService.GetUserWithBookmarkByIdAsync(userId);
            Console.WriteLine("hello world1");
            if (userProfile.User == null || userProfile.Bookmark == null)
            {
                return NotFound("User or bookmark not found");
            }

            var responseDto = new UserProfileResponse
            {
                User = new UserProfileDto
                {
                    UserId = userProfile.User.Id.ToString(),
                    FullName = userProfile.User.FullName,
                    UserName = userProfile.User.UserName,
                    Email = userProfile.User.Email
                },
                Bookmark = userProfile.Bookmark
            };

            return Ok(responseDto);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> EditUserProfile(string userId, [FromBody] UserEditRequest userEditRequest)
        {
            var success = await _userService.EditUserProfileAsync(userId, userEditRequest);

            if (!success)
            {
                return BadRequest("Failed to update user profile.");
            }

            return Ok("User profile updated successfully.");
        }
    }
}
