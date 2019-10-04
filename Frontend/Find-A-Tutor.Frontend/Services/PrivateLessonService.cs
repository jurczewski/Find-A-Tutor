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

        public async Task<Result<IEnumerable<PrivateLesson>>> GetAllAsync()
        {
            string url = UrlBasePath + Route;

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ResultSimple<IEnumerable<PrivateLesson>>>();

                    return Result<IEnumerable<PrivateLesson>>.Ok(result.Value);
                }
                else
                {
                    return Result<IEnumerable<PrivateLesson>>.Error("Cannot download privete lessons from API.");
                }
            }
        }
    }
}
