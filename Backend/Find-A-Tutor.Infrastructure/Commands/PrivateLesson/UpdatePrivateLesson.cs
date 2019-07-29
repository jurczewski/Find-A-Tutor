using System;

namespace Find_A_Tutor.Infrastructure.Commands.PrivateLesson
{
    public class UpdatePrivateLesson
    {
        public Guid PrivateLessonId { get; set; }
        public string Description { get; set; }
        public DateTime RelevantTo { get; set; }
        public string Subject { get; set; }
    }
}
