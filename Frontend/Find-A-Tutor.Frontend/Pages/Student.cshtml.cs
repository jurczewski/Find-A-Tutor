using System.Collections.Generic;
using System.Threading.Tasks;
using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Find_A_Tutor.Frontend.Pages
{
    public class StudentModel : PageModel
    {
        private readonly IAccountService _accountService;
        public Result<IEnumerable<PrivateLesson>> privateLessons;
        public StudentModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task OnGet()
        {
            var token = HttpContext.Session.GetString("token");
            var role = HttpContext.Session.GetString("role");

            if (token != null && role == "student")
            {
                privateLessons = await _accountService.GetLessonsForUser();
            }
            else if (role == "tutor")
            {
                Response.Redirect("Tutor");
            }
            else
            {
                Response.Redirect("Login");
            }
        }
    }
}