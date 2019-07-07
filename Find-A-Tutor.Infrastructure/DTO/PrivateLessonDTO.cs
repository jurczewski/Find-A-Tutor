using System;

namespace Find_A_Tutor.Infrastructure.DTO
{
    public class PrivateLessonDTO
    {
        public Guid Id { get; set; }
        public Guid StudnetId { get; protected set; }
        public Guid? TutorId { get; protected set; }
        //todo: odkomentować i przetestować
        //public Guid CreatedAt { get; protected set; } 
        public DateTime? TakenAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }
        public DateTime RelevantTo { get; protected set; }
        public string Description { get; protected set; }
        public string Subject { get; protected set; }
        public bool Taken => TutorId.HasValue;
        public bool IsPaid { get; protected set; }
        public bool IsDone { get; protected set; }
    }
}
