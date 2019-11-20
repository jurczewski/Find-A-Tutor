using System;

namespace Find_A_Tutor.Infrastructure.Commands.PrivateLesson
{
    public class AssignTutor
    {
        public Guid PrivateLessonId { get; set; }
        public double PricePerHour { get; set; }
    }
}
