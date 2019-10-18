using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Model.Account;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        private readonly IAccountService _accountService;
        public Result<TokenDto> Token { get; set; }
        public List<string> Errors { get; set; }

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
            Errors = new List<string>();
        }
        public async Task OnPost()
        {
            var response = await _accountService.Login(Email, Password);

            if (response.IsSuccess)
            {
                var data = response.Value;

                HttpContext.Session.SetString("token", data.Token);
                HttpContext.Session.SetString("role", data.Role);

                if (data.Role == "student")
                {
                    Response.Redirect("Student");
                }
                if (data.Role == "tutor")
                {
                    Response.Redirect("Tutor");
                }
                if (data.Role == "admin")
                {
                    Response.Redirect("Index");
                }
            }
            else
            {
                Errors.AddRange(response.Errors);
            }
        }
    }
}