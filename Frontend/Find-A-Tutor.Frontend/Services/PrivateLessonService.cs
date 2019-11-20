using Find_A_Tutor.Frontend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public class PrivateLessonService : IPrivateLessonService
    {
        readonly private string ApiUrl;
        readonly private string Route = "/privatelesson/";
        private readonly IHttpContextAccessor _accessor;

        public PrivateLessonService(IConfiguration config, IHttpContextAccessor accessor)
        {
            ApiUrl = config.GetValue<string>("ApiUrl");
            _accessor = accessor;
        }

        public async Task<Result<IEnumerable<PrivateLesson>>> GetAll()
        {
            var url = ApiUrl + Route;

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

        public async Task<Result<PrivateLesson>> Get(Guid privateLessonId)
        {
            var url = ApiUrl + Route + privateLessonId;

            using (var response = await ApiHelper.ApiClient.GetAsync(url))
            {            
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple<PrivateLesson>>();
                    return Result<PrivateLesson>.Ok(result.Value);
                }
                else
                {
                    return Result<PrivateLesson>.Error("Announcement was not found.");
                }
            }
        }

        public async Task<Result> Post(PrivateLesson privateLesson)
        {
            var url = ApiUrl + Route;
            var token = _accessor.HttpContext.Session.GetString("token");

            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await ApiHelper.ApiClient.PostAsJsonAsync(url, privateLesson))
            {
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple>();
                    return Result.Error(result.Errors.ToArray());
                }
                else
                {
                    return Result.Ok();
                }
            }
        }

        public async Task<Result> AssignTutor(AssignTutor tutor)
        {
            var url = ApiUrl + Route + "assign";
            var token = _accessor.HttpContext.Session.GetString("token");

            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await ApiHelper.ApiClient.PutAsJsonAsync(url, tutor))
            {
                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple>();
                    return Result.Error(result.Errors.ToArray());
                }
                else
                {
                    return Result.Ok();
                }
            }
        }

        public async Task<Result> RemoveAssignedTutor(string privateLessonId)
        {
            var url = ApiUrl + "/unassign/" + privateLessonId;
            var token = _accessor.HttpContext.Session.GetString("token");

            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await ApiHelper.ApiClient.PutAsync(url, new StringContent("")))
            {
                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple>();
                    return Result.Error(result.Errors.ToArray());
                }
                else
                {
                    return Result.Ok();
                }
            }
        }
    }
}
