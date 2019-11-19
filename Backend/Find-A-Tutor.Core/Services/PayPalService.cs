using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.PayPal;
using PayPalCheckoutSdk.Orders;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public class PayPalService : IPayPalService
    {
        private readonly PayPalClient _payPalClient;

        public PayPalService(PayPalClient payPalClient)
        {
            _payPalClient = payPalClient;
        }

        public async Task<Result<Order>> GetOrder(string orderId, bool debug = false)
        {
            var request = new OrdersGetRequest(orderId);
            var response = await _payPalClient.Client().Execute(request);
            var result = response.Result<Order>();
            //todo: Save things to DB

            return result != null ?
                                    Result<Order>.Ok(result) :
                                    Result<Order>.Error($"There are no payments with given Id.");
        }
    }
}
