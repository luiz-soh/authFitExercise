using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Login;
using Models.Dto.Token;
using Models.Dto.Error;
using Models.Dto.Login.Register;
using Application.Interfaces.User;
using Application.Interfaces.Gym;
using Models.Dto.Gym;
using Models.Dto.Gym.Register;

namespace Fitexerciselogin_api.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IGymService _gymService;

        public LoginController(IUserService userSerivce, IGymService gymService)
        {
            _userService = userSerivce;
            _gymService = gymService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginInput input)
        {
            input.Username = input.Username.Trim();
            var token = await _userService.SignIn(input);

            if (string.IsNullOrEmpty(token.UserToken))
            {
                if (token.UserId != 0)
                {
                    return Ok(token);
                }
                return NotFound(new ErrorOutput("usuario ou senha invalidos"));
            }

            return Ok(token);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpInput input)
        {
            input.Username = input.Username.Trim();
            var result = await _userService.SignUp(input);

            if (result != null)
            {  
                return BadRequest(result);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("UpdateToken")]
        public async Task<IActionResult> UpdateToken(UpdateTokenInput input)
        {
            var token = await _userService.UpdateToken(input);

            if (string.IsNullOrEmpty(token.UserToken))
                return NotFound(new TokenDTO());

            return Ok(token);
        }

        [HttpDelete("DeleteUser/{userId}")]
        [Authorize(Roles = "gym , adm")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var success = await _userService.DeleteUser(userId);

            if (success)
            {
                return Ok(success);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("GetGymToken")]
        public async Task<IActionResult> GetGymToken(GymLoginInput input)
        {
            input.Login = input.Login.Trim();
            var token = await _gymService.GetGymToken(input);

            if (string.IsNullOrEmpty(token.Token))
            {
                return NotFound(new ErrorOutput("usuario ou senha invalidos"));
            }

            return Ok(token);
        }

        [HttpPost("CreateGym")]
        public async Task<IActionResult> CreateGym(CreateGymInput input)
        {
            input.Login = input.Login.Trim();
            var result = await _gymService.CreateGym(input);

            if (result != null)
            {
                return BadRequest(result);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

    }
}