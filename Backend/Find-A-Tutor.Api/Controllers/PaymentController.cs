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
        [Route("/create")]
        public async Task<IActionResult> CreatePayment()
        {
            //todo: przekazać from body jakiś obiekt 
            logger.Info($"Creating payment against the PayPal API.");

            var result = await _paymentService.CreatePayment();

            logger.Info($"Created payment successfuly: '{result}' from the PayPal API.");

            foreach (var link in result.links)
            {
                if (link.rel.Equals("approval_url"))
                {
                    logger.Info($"Found the approval URL: {link.href} from response.");
                    return Redirect(link.href);
                }
            }

            return NotFound();
        }
    }
}
