using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Model.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public class AccountService : IAccountService
    {
        readonly private string ApiUrl;
        readonly private string Route = "/account/";
        readonly private IHttpContextAccessor _accessor;

        public AccountService(IConfiguration config, IHttpContextAccessor accessor)
        {
            ApiUrl = config.GetValue<string>("ApiUrl");
            _accessor = accessor;
        }

        public async Task<Result<TokenDto>> Login(string email, string password)
        {
            var login = new Login
            {
                Email = email,
                Password = password
            };

            var loginJson = JsonConvert.SerializeObject(login);

            var url = ApiUrl + Route + "login";

            using (var response = await ApiHelper.ApiClient.PostAsync(url, new StringContent(loginJson, Encoding.UTF8, "application/json")))
            {
                var result = await response.Content.ReadAsAsync<ResultSimple<TokenDto>>();

                if (result.IsSuccess)
                {                    
                    return Result<TokenDto>.Ok(result.Value);
                }
                else
                {
                    return Result<TokenDto>.Error(result.Errors.ToArray());
                }
            }
        }

        public async Task<Result> Register(Register register)
        {
            var loginJson = JsonConvert.SerializeObject(register);

            var url = ApiUrl + Route + "register";

            using (var response = await ApiHelper.ApiClient.PostAsync(url, new StringContent(loginJson, Encoding.UTF8, "application/json")))
            {
                if(response.StatusCode != HttpStatusCode.Created)
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple>();
                    return Result.Error(result.Errors.ToArray());
                }else
                {
                    return Result.Ok();
                }
            }
        }

        public async Task<Result<IEnumerable<PrivateLesson>>> GetLessonsForUser()
        {
            var url = ApiUrl + Route + "lessons";
            var token = _accessor.HttpContext.Session.GetString("token");

            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await ApiHelper.ApiClient.GetAsync(url))
            {
                var result = await response.Content.ReadAsAsync<ResultSimple<IEnumerable<PrivateLesson>>>();

                if (result.IsSuccess)
                {              
                    return Result<IEnumerable<PrivateLesson>>.Ok(result.Value);
                }
                else
                {
                    return Result<IEnumerable<PrivateLesson>>.Error(result.Errors.ToArray());
                }
            }
        }


    }
}
