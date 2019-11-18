using System;

namespace Find_A_Tutor.Infrastructure.Commands.Payment
{
    public class PaymentDetails
    {
        public string OrderID { get; set; }
        public Guid PrivateLessonId { get; set; }
    }
}
