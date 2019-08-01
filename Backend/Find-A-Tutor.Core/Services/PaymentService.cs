using Find_A_Tutor.Core.Settings;
using Microsoft.Extensions.Options;
using PayPal.Api;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PayPalSettings _payPalSettings;
        public PaymentService(IOptions<JwtSettings> payPalSettings)
        {
            //todo: payPalSettings.Value;
            //_payPalSettings = payPalSettings.Value;
        }
        public async Task<Payment> CreatePayment()
        {
            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            apiContext.Config = ConfigManager.Instance.GetProperties();

            return new Payment();
        }
    }
}
