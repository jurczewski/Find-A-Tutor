using BraintreeHttp;
using PayPalCheckoutSdk.Core;

namespace Find_A_Tutor.Core.PayPal
{
    public class PayPalClient
    {
        /// <summary>
        /// Set up PayPal environment with sandbox credentials. In production, use LiveEnvironment.
        /// </summary>
        /// <returns></returns>
        public static PayPalEnvironment Environment()
        {
            return new SandboxEnvironment("AX78qlQWBMJU0PIM9IEsKc0fbGRb6DNE6yJWTACOy-ww52uqn9gF0a3bAJiiHGzF4dYgT3l1JAQ2nhaf",
                "EJVLmrJ9AAWzuohekN-XqPGC5tAtrnvJgMx9hYgqc57QSn8JplxHKZ4e5KKE9Qf0G9H14hxEqcnACo7o");
        }

        /// <summary>
        /// Returns PayPalHttpClient instance to invoke PayPal APIs.
        /// </summary>
        /// <returns></returns>
        public static HttpClient Client()
        {
            return new PayPalHttpClient(Environment());
        }

        public static HttpClient Client(string refreshToken)
        {
            return new PayPalHttpClient(Environment(), refreshToken);
        }
    }
}
