using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Pages
{
    public class TutorModel : PageModel
    {
        private readonly IPrivateLessonService _privateLessonService;
        public Result<IEnumerable<PrivateLesson>> privateLessons;
        public List<string> Messages { get; set; }

        public TutorModel(IPrivateLessonService privateLessonService)
        {
            _privateLessonService = privateLessonService;
            Messages = new List<string>();
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

        public async Task OnGetAssignTutor(Guid id)
        {
            var result = await _privateLessonService.AssignTutor(id.ToString());
            privateLessons = await _privateLessonService.GetAll();

            if (result.IsSuccess)
            {
                Messages.Add("Announcement was assinged to your account 😊");
            }
            else
            {
                Messages.AddRange(result.Errors);
            }
        }

        public async Task OnGetRemoveAssignedTutor(Guid id)
        {
            var result = await _privateLessonService.RemoveAssignedTutor(id.ToString());
            privateLessons = await _privateLessonService.GetAll();

            if (result.IsSuccess)
            {
                Messages.Add("Announcement was removed from your account 😊");
            }
            else
            {
                Messages.AddRange(result.Errors);
            }
        }
    }
}