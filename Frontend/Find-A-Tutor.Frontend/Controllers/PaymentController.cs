using Find_A_Tutor.Frontend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Controllers
{
    [Route("")]
    public class PaymentController : Controller
    {
        [HttpPost("pay")]
        public async Task<Result> UpdatePaymentStatusToPaid([FromBody]PaymentDetails paymentDetails)
        {
            var url = "http://localhost:5000" + "/Payment/paypal-transaction-complete";
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
