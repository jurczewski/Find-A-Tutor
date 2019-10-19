using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Pages
{
    public class NewLessonModel : PageModel
    {
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string Subject { get; set; }
        [BindProperty]
        public DateTime RelevantTo { get; set;}
        public List<SchoolSubject> SchoolSubjects { get; set; }

        public List<string> Messages { get; set; }
        private readonly IPrivateLessonService _privateLessonService;
        private readonly ISchoolSubjectService _schoolSubjectService;

        public NewLessonModel(IPrivateLessonService privateLessonService, ISchoolSubjectService schoolSubjectService)
        {
            _privateLessonService = privateLessonService;
            _schoolSubjectService = schoolSubjectService;
            Messages = new List<string>();
        }

        public async Task OnGet()
        {
            var token = HttpContext.Session.GetString("token");
            var role = HttpContext.Session.GetString("role");

            if (token == null && role == "student")
            {
                Response.Redirect("Student");
            }
            if (role == "tutor")
            {
                Response.Redirect("Tutor");
            }
            if (token == null)
            {
                Response.Redirect("Login");
            }

            var response = await _schoolSubjectService.GetAll();

            if (response.IsSuccess)
            {
                SchoolSubjects = response.Value.ToList();
            }else
            {
                Messages.AddRange(response.Errors);
            }
        }

        public async Task OnPost()
        {
            var response = await _privateLessonService.Post(new PrivateLesson
            {
                Description = Description,
                Subject = Subject,
                RelevantTo = RelevantTo
            });

            if (response.IsSuccess)
            {
                Messages.Add("Dodano lekcje 😊");
            }
            else
            {
                Messages.AddRange(response.Errors);
            }
        }
    }
}