using Find_A_Tutor.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Find_A_Tutor.Api.Controllers
{
    [Route("[controller]")]
    public class PrivateLessonController : Controller
    {
        private readonly IPrivateLessonService _privateLessonService;
        public PrivateLessonController(IPrivateLessonService privateLessonService)
        {
            _privateLessonService = privateLessonService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string description)
        {
            var events = await _privateLessonService.BrowseAsync(description);

            return Json(events);
        }
    }
}
