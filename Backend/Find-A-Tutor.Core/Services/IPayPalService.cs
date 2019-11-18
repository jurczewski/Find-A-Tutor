using Find_A_Tutor.Core.Domain;
using PayPalCheckoutSdk.Orders;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public interface IPayPalService
    {
        Task<Result<Order>> GetOrder(string orderId, bool debug = false);
    }
}
