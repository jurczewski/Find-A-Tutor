using Find_A_Tutor.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Threading.Tasks;

namespace Find_A_Tutor.Api.Controllers
{
    [Route("[controller]")]
    public class PaymentController : ApiControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> CreatePayment()
        {
            //todo: przekazać from body jakiś obiekt 
            logger.Info($"Creating payment against the PayPal API.");

            var result = await _paymentService.CreatePayment();

            if (result.IsSuccess)
            {
                foreach (var link in result.Value.links)
                {
                    if (link.rel.Equals("approval_url"))
                    {
                        return Redirect(link.href);
                    }
                }
            }

            return Json(result);
        }

        [HttpGet]
        [Route("success")]
        public async Task<IActionResult> ExecutePayment(string paymentId, string payerId)
        {
            logger.Info($"Executing the payment against the Paypal API.");

            var result = await _paymentService.ExecutePayment(paymentId, payerId);

            logger.Info($"The PayPal Controller has a new response: '{result}' from the PayPal Api.");

            return Json(result);
        }
    }
}
