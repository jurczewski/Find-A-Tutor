using Find_A_Tutor.Frontend.Model;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public interface IPaymentService
    {
        Task<Result> UpdatePaymentStatusToPaid(PaymentDetails paymentDetails);
    }
}
