using Find_A_Tutor.Frontend.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public class PrivateLessonService : IPrivateLessonService
    {
        readonly static string UrlBasePath = "http://localhost:5000";
        readonly static string Route = "/privatelesson/";

        public async Task<Result<IEnumerable<PrivateLesson>>> GetAll()
        {
            var url = UrlBasePath + Route;

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

        public async Task<Result<PrivateLesson>> Get(Guid privateLessonId)
        {
            var url = UrlBasePath + Route + privateLessonId;

            using (var response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple<PrivateLesson>>();

                    return Result<PrivateLesson>.Ok(result.Value);
                }
                else
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple<IEnumerable<PrivateLesson>>>();

                    return Result<PrivateLesson>.Error(result.Errors.ToArray());
                }
            }
        }
    }
}
