using application.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using models.Dto.Login;
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
            if (string.IsNullOrEmpty(input.Username) || string.IsNullOrEmpty(input.Password))
                return BadRequest(new { message = "usuario e senha obrigatórios" });

            var token = await _authentication.SignIn(input);

            if (string.IsNullOrEmpty(token.UserToken))
                return NotFound(new { message = "usuario ou senha invalidos" });

            return Ok(token);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpInput input)
        {
            //TODO
            //Pensar em usar fluentValidation

            if (input.Password != input.ConfirmPassword)
                return BadRequest(new { message = "Senhas não batem" });
            if (input.Password.Length < 8 || !input.Password.Any(c => char.IsDigit(c)) ||
            !input.Password.Any(c => char.IsLower(c)) || !input.Password.Any(c => char.IsUpper(c)) ||
            !input.Password.Any(c => !char.IsLetterOrDigit(c)))
            {
                return BadRequest(new { message = "Senha muito fraca" });
            }

            await _authentication.SignUp(input);

            return Ok();
        }

        [HttpPut("UpdateToken")]
        public async Task<IActionResult> UpdateToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest(new { message = "usuario e senha obrigatórios" });

            var token = await _authentication.UpdateToken(refreshToken);

            if (string.IsNullOrEmpty(token.UserToken))
                return NotFound(new { message = "usuario ou senha invalidos" });

            return Ok(token);
        }

    }
}