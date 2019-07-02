using System;

namespace Find_A_Tutor.Core.Domain
{
    public class PrivateLesson : Entity
    {
        public Guid StudnetId { get; protected set; }
        public Guid? TutorId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? TakenAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }
        public DateTime RelevantTo { get; protected set; }
        public string Description { get; protected set; }
        public SchoolSubject Subject { get; protected set; }
        public bool Taken => TutorId.HasValue;
        public bool IsPaid { get; protected set; }
        public bool IsDone { get; protected set; }

        public PrivateLesson(Guid studnetId, DateTime relevantTo, string description, SchoolSubject subject)
        {
            StudnetId = studnetId;
            RelevantTo = relevantTo;
            Description = description;
            Subject = subject;
            CreatedAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
            IsPaid = false;
            IsDone = false;
        }

        public void Take(User tutor)
        {
            if (Taken)
            {
                throw new Exception($"Lesson was already taken by another tutor at: {TakenAt}.");
            }
            TutorId = tutor.Id;
            TakenAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (!Taken)
            {
                throw new Exception($"Ticket was not purchased and cannot be canceled.");
            }
            TutorId = null;
            TakenAt = null;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
