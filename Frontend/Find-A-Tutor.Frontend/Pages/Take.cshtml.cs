using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Pages
{
    public class TakeModel : PageModel
    {
        private readonly IPrivateLessonService _privateLessonService;
        public Result<PrivateLesson> privateLesson;
        public List<string> Messages { get; set; }

        [BindProperty]
        public double PricePerHour { get; set; }

        public TakeModel(IPrivateLessonService privateLessonService)
        {
            _privateLessonService = privateLessonService;
            Messages = new List<string>();
        }
        public async Task OnGetAsync(Guid id)
        {
            if (id != Guid.Empty)
            {
                privateLesson = await _privateLessonService.Get(id);
            }
            else
            {
                Redirect("Tutor");
            }
        }

        public async Task OnPostAssignTutor(Guid id)
        {
            var result = await _privateLessonService.AssignTutor(new AssignTutor
            { 
                PricePerHour = PricePerHour,
                PrivateLessonId = id
            });

            if (result.IsSuccess)
            {
                Messages.Add("Announcement was assinged to your account 😊");
            }
            else
            {
                Messages.AddRange(result.Errors);
            }

            privateLesson = await _privateLessonService.Get(id);
        }
    }
}