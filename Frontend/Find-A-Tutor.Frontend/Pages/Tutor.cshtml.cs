using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Pages
{
    public class TutorModel : PageModel
    {
        private readonly IPrivateLessonService _privateLessonService;
        public Result<IEnumerable<PrivateLesson>> privateLessons;

        public TutorModel(IPrivateLessonService privateLessonService)
        {
            _privateLessonService = privateLessonService;
        }

        public async Task OnGet()
        {
            var token = HttpContext.Session.GetString("token");
            var role = HttpContext.Session.GetString("role");

            if (token != null && role == "tutor")
            {
                privateLessons = await _privateLessonService.GetAll();
            }
            else if (role == "student")
            {
                Response.Redirect("Student");
            }
            else
            {
                Response.Redirect("Login");
            }
        }
    }
}