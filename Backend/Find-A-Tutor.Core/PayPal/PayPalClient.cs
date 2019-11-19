using BraintreeHttp;
using Microsoft.Extensions.Configuration;
using PayPalCheckoutSdk.Core;

namespace Find_A_Tutor.Core.PayPal
{
    public class PayPalClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public PayPalClient(IConfiguration config)
        {
            _clientId = config.GetValue<string>("paypal:clientId");
            _clientSecret = config.GetValue<string>("paypal:clientSecret");
        }

        /// <summary>
        /// Set up PayPal environment with sandbox credentials. In production, use LiveEnvironment.
        /// </summary>
        /// <returns></returns>
        public PayPalEnvironment Environment()
        {
            return new SandboxEnvironment(_clientId, _clientSecret);
        }

        /// <summary>
        /// Returns PayPalHttpClient instance to invoke PayPal APIs.
        /// </summary>
        /// <returns></returns>
        public HttpClient Client()
        {
            return new PayPalHttpClient(Environment());
        }

        public HttpClient Client(string refreshToken)
        {
            return new PayPalHttpClient(Environment(), refreshToken);
        }
    }
}
