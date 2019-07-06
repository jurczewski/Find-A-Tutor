using System;

namespace Find_A_Tutor.Core.Domain
{
    public class PrivateLesson : Entity
    {
        public Guid StudnetId { get; protected set; }
        public Guid? TutorId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? TakenAt { get; protected set; }
        public DateTime? UpdateAt { get; protected set; }
        public DateTime RelevantTo { get; protected set; }
        public string Description { get; protected set; }
        public SchoolSubject Subject { get; protected set; }
        public bool IsAssigned => TutorId.HasValue;
        public bool IsPaid { get; protected set; }
        public bool IsDone { get; protected set; }

        public PrivateLesson(Guid id, Guid studnetId, DateTime relevantTo, string description, string subject)
        {
            Id = id;
            StudnetId = studnetId;
            CreatedAt = DateTime.UtcNow;
            SetRelevantToDate(relevantTo);
            SetDesctiption(description);
            TryParseStringToSubjectEnum(subject);
            UpdateAt = null;
            IsPaid = false;
            IsDone = false;
        }

        public void TryParseStringToSubjectEnum(string subject)
        {
            Enum.TryParse(subject, true, out SchoolSubject Subject);
        }

        public void SetDesctiption(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new Exception($"Private Lesson can not have an empty description.");
            }
            Description = description;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetRelevantToDate(DateTime relevantTo)
        {
            if (CreatedAt >= relevantTo)
            {
                throw new Exception($"Private lesson must have a relevent-to date greater than created date.");
            }
            RelevantTo = relevantTo;
            UpdateAt = DateTime.UtcNow;
        }

        public void AssignTutor(User tutor)
        {
            if (IsAssigned)
            {
                throw new Exception($"Private lesson was already taken by another tutor at: {TakenAt}.");
            }
            TutorId = tutor.Id;
            TakenAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void RemoveAssignedTutor()
        {
            if (!IsAssigned)
            {
                throw new Exception($"You cannot untake an private lesson that was not taken before.");
            }
            TutorId = null;
            TakenAt = null;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
