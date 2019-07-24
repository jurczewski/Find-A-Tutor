using System;

namespace Find_A_Tutor.Core.DTO
{
    public class PrivateLessonDTO
    {
        public Guid Id { get; set; }
        public Guid StudnetId { get; set; }
        public Guid? TutorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? TakenAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime RelevantTo { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public bool IsAssigned => TutorId.HasValue;
        public bool IsPaid { get; set; }
        public bool IsDone { get; set; }
    }
}
