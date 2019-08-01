using PayPal.Api;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public interface IPaymentService
    {
        Task<Payment> CreatePayment();
    }
}
