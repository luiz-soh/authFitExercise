using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthExercise_api.Controllers.HealthCheck
{
    [Route("[controller]")]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}

