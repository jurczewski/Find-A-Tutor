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
        public readonly string _payPalClientId;
        public Result<PrivateLesson> privateLesson;

        public DetailsModel(IPrivateLessonService privateLessonService, IConfiguration configuration)
        {
            _privateLessonService = privateLessonService;
            _payPalClientId = configuration.GetValue<string>("PayPal:client-id");
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