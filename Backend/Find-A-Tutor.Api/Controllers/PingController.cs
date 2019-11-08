using Microsoft.AspNetCore.Mvc;

namespace Find_A_Tutor.Api.Controllers
{
    [Route("[controller]")]
    public class PingController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("pong");
        }
    }
}
