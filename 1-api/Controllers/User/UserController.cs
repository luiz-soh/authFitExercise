using System.Text.RegularExpressions;
using Amazon.Runtime.Internal;
using application.Interfaces.Authentication;
using Application.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Login;
using Models.Dto.Token;
using Models.Dto.Error;
using Models.Dto.Login.Register;
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

        [HttpPut("AddUserEmail")]
        [Authorize]
        public async Task<IActionResult> AddUserEmail([FromBody] AddUserEmailInput input)
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
    }
}