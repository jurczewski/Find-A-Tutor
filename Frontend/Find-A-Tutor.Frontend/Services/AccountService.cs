using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Model.Account;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public class AccountService : IAccountService
    {
        readonly string UrlBasePath;
        readonly static string Route = "/account/";

        public AccountService(IConfiguration config)
        {
            UrlBasePath = config.GetValue<string>("UrlBasePath");
        }

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
        public async Task<Result<IEnumerable<PrivateLesson>>> GetLessonsForUser(string token)
        {
            var url = UrlBasePath + Route + "lessons";

            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple<IEnumerable<PrivateLesson>>>();

                    return Result<IEnumerable<PrivateLesson>>.Ok(result.Value);
                }
                else
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple<IEnumerable<PrivateLesson>>>();

                    return Result<IEnumerable<PrivateLesson>>.Error(result.Errors.ToArray());
                }
            }
        }


    }
}
