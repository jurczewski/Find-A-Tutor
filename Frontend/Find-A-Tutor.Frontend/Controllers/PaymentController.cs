using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Controllers
{
    [Route("")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("pay")]
        public async Task<IActionResult> UpdatePaymentStatusToPaid([FromBody]PaymentDetails paymentDetails)
        {
            var responsePayPal = await _paymentService.UpdatePaymentStatusToPaid(paymentDetails);

            return Ok(responsePayPal);
        }
    }
}
