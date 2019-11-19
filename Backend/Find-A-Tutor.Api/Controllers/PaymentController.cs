using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Services;
using Find_A_Tutor.Infrastructure.Commands.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Find_A_Tutor.Api.Controllers
{
    [Route("[controller]")]
    public class PaymentController : ApiControllerBase
    {
        private readonly IPayPalService _payPalService;
        private readonly IPrivateLessonService _privateLessonService;

        public PaymentController(IPayPalService payPalService, IPrivateLessonService privateLessonService)
        {
            _payPalService = payPalService;
            _privateLessonService = privateLessonService;
        }

        [Authorize(Policy = "HasStudentRole")]
        [HttpPost("paypal-transaction-complete")]
        public async Task<IActionResult> Post([FromBody]PaymentDetails paymentDetails)
        {
            var responsePayPal = await _payPalService.GetOrder(paymentDetails.OrderID);

            if (responsePayPal.Value.Status == "COMPLETED")
            {
                var response = await _privateLessonService.UpdatePaymentStatusToPaid(paymentDetails.PrivateLessonId, UserId);
                return response.IsSuccess ? Created($"/PrivateLesson/{paymentDetails.PrivateLessonId}", null) : (IActionResult)Ok(response);
            }
            else
            {
                return Ok(responsePayPal);
            }
        }

        [Authorize(Policy = "HasAdminRole")]
        [HttpGet("order-details/{orderID}")]
        public async Task<IActionResult> Get(string orderID)
        {
            var responsePayPal = await _payPalService.GetOrder(orderID);
            return responsePayPal.IsSuccess ? (IActionResult)Ok(responsePayPal) : NotFound();
        }
    }
}
