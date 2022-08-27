using application.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using models.Dto.Login;
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
            //TODO
            //Pensar em usar fluentValidation

            if (!input.Password.Any(c => char.IsDigit(c)) || !input.Password.Any(c => char.IsLower(c)) ||
            !input.Password.Any(c => char.IsUpper(c)) || !input.Password.Any(c => !char.IsLetterOrDigit(c)))
            {
                return BadRequest(new ErrorOutput("Senha muito fraca"));
            }

            var result = await _authentication.SignUp(input);

            if (result != null)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpPut("UpdateToken")]
        public async Task<IActionResult> UpdateToken(UpdateTokenInput input)
        {
            var token = await _authentication.UpdateToken(input);

            if (string.IsNullOrEmpty(token.UserToken))
                return NotFound(new ErrorOutput("usuario ou senha invalidos"));

            return Ok(token);
        }

    }
}