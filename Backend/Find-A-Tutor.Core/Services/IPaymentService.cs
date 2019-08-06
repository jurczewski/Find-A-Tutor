using Find_A_Tutor.Core.Domain;
using PayPal.v1.Payments;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public interface IPaymentService
    {
        Task<Result<Payment>> CreatePayment();
        Task<Result<Payment>> ExecutePayment(string paymentId, string payerId);
    }
}
