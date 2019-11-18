using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.PayPal;
using PayPalCheckoutSdk.Orders;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public class PayPalService : IPayPalService
    {
        public async Task<Result<Order>> GetOrder(string orderId, bool debug = false)
        {
            var request = new OrdersGetRequest(orderId);
            var response = await PayPalClient.Client().Execute(request);
            var result = response.Result<Order>();
            //todo: Save things to DB

            return result != null ?
                                    Result<Order>.Ok(result) :
                                    Result<Order>.Error($"There are no payments with given Id.");
        }
    }
}
