using application.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using models.Dto.Login;

namespace web_api.Controllers.Login
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

        //TODO
        //Cria inputs separados para SignIn e SignUp
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
        public async Task<IActionResult> SignUp(LoginInput input)
        {
            if (string.IsNullOrEmpty(input.Username) || string.IsNullOrEmpty(input.Password))
                return BadRequest(new { message = "usuario e senha obrigatórios" });

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