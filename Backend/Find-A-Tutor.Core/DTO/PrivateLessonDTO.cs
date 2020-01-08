using System;

namespace Find_A_Tutor.Core.DTO
{
    public class PrivateLessonDTO
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid? TutorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? TakenAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime RelevantTo { get; set; }
        public string Description { get; set; }
        public Guid Subject { get; set; }
        public bool IsAssigned => TutorId.HasValue;
        public bool IsPaid { get; set; }
        public bool IsDone { get; set; }
        public double Time { get; set; }
        public double PricePerHour { get; set; }
        public double TotalPrice => Time * PricePerHour;
    }
}
