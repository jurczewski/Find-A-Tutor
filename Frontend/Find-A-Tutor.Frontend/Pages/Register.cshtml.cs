using System.Collections.Generic;
using System.Threading.Tasks;
using Find_A_Tutor.Frontend.Model.Account;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Find_A_Tutor.Frontend.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public bool IsTutor { get; set; }

        public List<string> Messages { get; set; }
        private readonly IAccountService _accountService;

        public RegisterModel(IAccountService accountService)
        {
            _accountService = accountService;
            Messages = new List<string>();
        }
        public async Task OnPost()
        {
            var response = await _accountService.Register(new Register
            {
                Email = Email,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                Role = IsTutor ? "tutor" : "student"
            });

            if (response.IsSuccess)
            {
                Messages.Add("Your account was created 😊");
            }
            else
            {
                Messages.AddRange(response.Errors);
            }
        }
    }
}