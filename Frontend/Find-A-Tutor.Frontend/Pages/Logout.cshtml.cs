using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace Find_A_Tutor.Frontend.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.SetString("token", "");
            Response.Redirect("Index");
        }
    }
}