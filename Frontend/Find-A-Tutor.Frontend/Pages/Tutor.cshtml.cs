using System.Collections.Generic;
using System.Threading.Tasks;
using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async Task OnGetAsync()
        {
            privateLessons = await _privateLessonService.GetAll();
        }
    }
}