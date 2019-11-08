using Microsoft.AspNetCore.Mvc;

namespace Find_A_Tutor.Api.Controllers
{
    [Route("")]
    public class HomeController : ApiControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Find-A-Tutor.Api");
        }
    }
}
