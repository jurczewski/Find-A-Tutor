using System;
using System.Threading.Tasks;
using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Find_A_Tutor.Frontend.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IPrivateLessonService _privateLessonService;
        public Result<PrivateLesson> privateLesson;
        public string ApiUrl { get; }

        public DetailsModel(IPrivateLessonService privateLessonService, IConfiguration config)
        {
            _privateLessonService = privateLessonService;
            ApiUrl = config.GetValue<string>("ApiUrl");
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