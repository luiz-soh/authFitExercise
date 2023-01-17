using System.Text.RegularExpressions;
using application.Interfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Login;
using Models.Dto.Token;
using Models.Dto.Error;
using Models.Dto.Login.Register;

namespace Fitexerciselogin_api.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthentication _authentication;

        public LoginController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginInput input)
        {
            input.Username = input.Username.Trim();
            var token = await _authentication.SignIn(input);

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
            var result = await _authentication.SignUp(input);

            if (result != null)
            {
                return BadRequest(result);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("UpdateToken")]
        public async Task<IActionResult> UpdateToken(UpdateTokenInput input)
        {
            var token = await _authentication.UpdateToken(input);

            if (string.IsNullOrEmpty(token.UserToken))
                return NotFound(new TokenDTO());

            return Ok(token);
        }

        [HttpDelete("DeleteUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var success = await _authentication.DeleteUser(userId);

            if (success)
            {
                return Ok(success);
            }
            else
            {
                return NotFound();
            }
        }

    }
}