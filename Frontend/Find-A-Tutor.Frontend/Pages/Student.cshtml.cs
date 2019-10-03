using System.Collections.Generic;
using System.Threading.Tasks;
using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Find_A_Tutor.Frontend.Pages
{
    public class StudentModel : PageModel
    {
        private readonly IPrivateLessonService _privateLessonService;
        public IEnumerable<PrivateLesson> privateLessons;

        public StudentModel(IPrivateLessonService privateLessonService)
        {
            _privateLessonService = privateLessonService;
        }

        public async Task OnGetAsync()
        {
            privateLessons = await _privateLessonService.GetAllAsync();
        }
    }
}