using Find_A_Tutor.Frontend.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public class SchoolSubjectService : ISchoolSubjectService
    {
        readonly private string ApiUrl;
        readonly private string Route = "/schoolsubject/";

        public SchoolSubjectService(IConfiguration config)
        {
            ApiUrl = config.GetValue<string>("ApiUrl");
        }

        public async Task<Result<IEnumerable<SchoolSubject>>> GetAll()
        {
            var url = ApiUrl + Route;

            using (var response = await ApiHelper.ApiClient.GetAsync(url))
            {
                var result = await response.Content.ReadAsAsync<ResultSimple<IEnumerable<SchoolSubject>>>();

                if (result.IsSuccess)
                {
                    return Result<IEnumerable<SchoolSubject>>.Ok(result.Value);
                }
                else
                {
                    return Result<IEnumerable<SchoolSubject>>.Error(result.Errors.ToArray());
                }
            }
        }

    }
}
