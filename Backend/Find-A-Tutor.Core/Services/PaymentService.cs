using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Settings;
using Microsoft.Extensions.Options;
using NLog;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly PayPalSettings _payPalSettings;
        private readonly Dictionary<string, string> _sdkConfig;
        private readonly string _accessToken;
        public PaymentService(IOptions<PayPalSettings> payPalSettings)
        {
            _payPalSettings = payPalSettings.Value;
            _sdkConfig = new Dictionary<string, string> {
               { "mode", "sandbox" },
               { "clientId", _payPalSettings.ClientId },
               { "clientSecret", _payPalSettings.ClientSecret }
            };
            _accessToken = new OAuthTokenCredential(_payPalSettings.ClientId, _payPalSettings.ClientSecret, _sdkConfig).GetAccessToken();
        }
        public async Task<Result<Payment>> CreatePayment()
        {

            var apiContext = new APIContext(_accessToken)
            {
                Config = _sdkConfig
            };

            logger.Info($"Creating payment...");
            try
            {
                //todo: fulfill payment with data
                var payment = Payment.Create(apiContext, new Payment
                {
                    intent = "sale",
                    payer = new Payer
                    {
                        payment_method = "paypal"
                    },
                    transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "Transaction description.",
                        invoice_number = "001",
                        amount = new Amount
                        {
                            currency = "USD",
                            total = "100.00",
                            details = new Details
                            {
                                tax = "15",
                                shipping = "10",
                                subtotal = "75"
                            }
                        },
                        item_list = new ItemList
                        {
                            items = new List<Item>
                            {
                                new Item
                                {
                                    name = "Item Name",
                                    currency = "USD",
                                    price = "15",
                                    quantity = "5",
                                    sku = "sku"
                                }
                            }
                        }
                    }
                },
                    redirect_urls = new RedirectUrls
                    {
                        return_url = "http://mysite.com/return",
                        cancel_url = "http://mysite.com/cancel"
                    }
                });

                var createdPayment = await Task.Run(() => payment.Create(apiContext));

                logger.Info($"Payment created succesfully '{createdPayment}'");

                return Result<Payment>.Ok(createdPayment);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "An error occurred while creating an invoice.");

                return Result<Payment>.Error("An error occurred while creating an invoice.");
            }               
        }

        public async Task<Result<Payment>> ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = new APIContext(_accessToken)
            {
                Config = _sdkConfig
            };

            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };
            var executedPayment = await Task.Run(() => payment.Execute(apiContext, paymentExecution));

            return Result<Payment>.Ok(executedPayment);
        }
    }
}
