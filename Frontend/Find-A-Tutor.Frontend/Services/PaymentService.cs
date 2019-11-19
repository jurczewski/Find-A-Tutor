using Find_A_Tutor.Frontend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public class PaymentService : IPaymentService
    {
        readonly private string ApiUrl;
        readonly private string Route = "/payment/";
        private readonly IHttpContextAccessor _accessor;

        public PaymentService(IConfiguration config, IHttpContextAccessor accessor)
        {
            ApiUrl = config.GetValue<string>("ApiUrl");
            _accessor = accessor;
        }

        public async Task<Result> UpdatePaymentStatusToPaid(PaymentDetails paymentDetails)
        {
            var url = ApiUrl + Route + "paypal-transaction-complete";
            //var token = _accessor.HttpContext.Session.GetString("token");

            //ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await ApiHelper.ApiClient.PostAsJsonAsync(url, paymentDetails))
            {
                if (response.StatusCode != HttpStatusCode.OK)
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
