using Application.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.User;

namespace Fitexerciselogin_api.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("UpdateUserEmail")]
        [Authorize]
        public async Task<IActionResult> UpdateUserEmail([FromBody] AddUserEmailInput input)
        {
            await _userService.AddUserEmail(input);
            return Ok();
        }

        [HttpGet("GetUserData/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserData([FromRoute] int userId)
        {
            var user = await _userService.GetUserData(userId);

            if (user.Id == 0)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("GetUserByGymId/{gymId}")]
        [Authorize(Roles = "gym")]
        public async Task<IActionResult> GetUserByGymId([FromRoute] int gymId)
        {
            var users = await _userService.GetUsersByGymId(gymId);

            return Ok(users);
        }
    }
}