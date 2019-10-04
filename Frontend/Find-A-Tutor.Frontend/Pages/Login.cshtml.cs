using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Model.Account;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Pages
{
    public class LoginModel : PageModel
    {
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
            string email = Request.Form["email"];
            string password = Request.Form["password"];

            var response = await _accountService.Login(email, password);
            if (response.IsSuccess)
            {
                HttpContext.Session.SetString("token", response.Value.Token);
                Errors.Add(response.Value.Token);
            }
            else
            {
                Errors.AddRange(response.Errors);
            }
        }
    }
}