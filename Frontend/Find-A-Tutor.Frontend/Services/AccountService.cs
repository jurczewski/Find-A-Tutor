using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Model.Account;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public class AccountService : IAccountService
    {
        readonly static string UrlBasePath = "http://localhost:5000";
        readonly static string Route = "/account/";

        public async Task<Result<TokenDto>> Login(string email, string password)
        {
            var login = new Login
            {
                Email = email,
                Password = password
            };

            var loginJson = JsonConvert.SerializeObject(login);

            var url = UrlBasePath + Route + "login";

            using (var response = await ApiHelper.ApiClient.PostAsync(url, new StringContent(loginJson, Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple<TokenDto>>();

                    return Result<TokenDto>.Ok(result.Value);
                }
                else
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple<TokenDto>>();

                    return Result<TokenDto>.Error(result.Errors.ToArray());
                }
            }
        }

    }
}
