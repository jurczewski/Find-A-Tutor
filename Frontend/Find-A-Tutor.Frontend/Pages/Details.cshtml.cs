using System;
using System.Threading.Tasks;
using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Find_A_Tutor.Frontend.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IPrivateLessonService _privateLessonService;
        public Result<PrivateLesson> privateLesson;

        public DetailsModel(IPrivateLessonService privateLessonService)
        {
            _privateLessonService = privateLessonService;
        }

        public async Task OnGet(Guid id)
        {
            if(id != Guid.Empty)
            {
                privateLesson = await _privateLessonService.Get(id);
            }else
            {
                Redirect("Student");
            }            
        }
    }
}