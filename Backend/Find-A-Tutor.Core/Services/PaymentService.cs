using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Settings;
using Microsoft.Extensions.Options;
using NLog;
using PayPal.Core;
using PayPal.v1.Payments;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly PayPalSettings _payPalSettings;
        private readonly PayPalHttpClient client;
        private readonly SandboxEnvironment environment;

        public PaymentService(IOptions<PayPalSettings> payPalSettings)
        {
            _payPalSettings = payPalSettings.Value;
            environment = new SandboxEnvironment(_payPalSettings.ClientId, _payPalSettings.ClientSecret);
            client = new PayPalHttpClient(environment);
        }
        public async Task<Result<Payment>> CreatePayment()
        {
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                        new Transaction()
                        {
                            Amount = new Amount()
                            {
                                Total = "10",
                                Currency = "PLN"
                            }
                        }
                },
                RedirectUrls = new RedirectUrls()
                {
                    ReturnUrl = "https://www.example.com/",
                    CancelUrl = "https://www.example.com"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            HttpStatusCode statusCode;

            try
            {
                BraintreeHttp.HttpResponse response = await client.Execute(request);
                statusCode = response.StatusCode;
                var result = response.Result<Payment>();
                return Result<Payment>.Ok(result);
            }
            catch (BraintreeHttp.HttpException httpException)
            {
                statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                return Result<Payment>.Error(statusCode.ToString(), debugId);
            }
        }

        public async Task<Result<Payment>> ExecutePayment(string paymentId, string payerId)
        {
            var request = new PaymentExecuteRequest(paymentId);
            request.RequestBody(new PaymentExecution()
            {
                PayerId = payerId
            });

            try
            {
                BraintreeHttp.HttpResponse response = await client.Execute(request);
                var statusCode = response.StatusCode;
                var result = response.Result<Payment>();
                return Result<Payment>.Ok(result);

            }
            catch (BraintreeHttp.HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                return Result<Payment>.Error(statusCode.ToString(), debugId);
            }
        }
    }
}
