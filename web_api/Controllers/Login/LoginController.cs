using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using models.Dto.Login;

namespace web_api.Controllers.Login
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAuthentication _authentication;

        public LoginController(ILogger<LoginController> logger, IAuthentication authentication)
        {
            _logger = logger;
            _authentication = authentication;
        }

        [HttpPost("RealizaLogin")]
        public async Task<IActionResult> RealizaLogin(LoginInput input)
        {
            if (string.IsNullOrEmpty(input.Username) || string.IsNullOrEmpty(input.Password))
                return BadRequest(new { message = "usuario e senha obrigatórios" });

            var token = await _authentication.RealizaLogin(input);

            if (string.IsNullOrEmpty(token.User_Token))
                return NotFound(new { message = "usuario ou senha invalidos" });

            return Ok(token);
        }

        [HttpPost("CriaLogin")]
        public async Task<IActionResult> CriaLogin(LoginInput input)
        {
            if (string.IsNullOrEmpty(input.Username) || string.IsNullOrEmpty(input.Password))
                return BadRequest(new { message = "usuario e senha obrigatórios" });

            await _authentication.CriaLogin(input);

            return Ok();
        }

    }
}