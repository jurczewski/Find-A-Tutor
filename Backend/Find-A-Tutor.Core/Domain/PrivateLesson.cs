using Find_A_Tutor.Core.Exceptions;
using System;

namespace Find_A_Tutor.Core.Domain
{
    public class PrivateLesson : Entity
    {
        public Guid StudentId { get; protected set; }
        public Guid? TutorId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? TakenAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime RelevantTo { get; protected set; }
        public string Description { get; protected set; }
        public SchoolSubject SchoolSubject { get; protected set; }
        public bool IsAssigned => TutorId.HasValue;
        public bool IsPaid { get; protected set; }
        public bool IsDone { get; protected set; }
        public double Time { get; protected set; }
        public double PricePerHour { get; protected set; }
        public double TotalPrice => Time * PricePerHour;

        public PrivateLesson() { }

        public PrivateLesson(Guid id, Guid studnetId, DateTime relevantTo, string description, SchoolSubject subject, double time)
        {
            Id = id;
            StudentId = studnetId;
            CreatedAt = DateTime.UtcNow;
            SetRelevantToDate(relevantTo);
            SetDesctiption(description);
            SchoolSubject = subject;
            UpdatedAt = null;
            IsPaid = false;
            IsDone = false;
            SetTime(time);
        }

        public void SetSchoolSubject(SchoolSubject schoolSubject)
        {
            SchoolSubject = schoolSubject;
        }

        public void SetDesctiption(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ValidationException($"Private Lesson can not have an empty description.");
            }
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRelevantToDate(DateTime relevantTo)
        {
            if (CreatedAt >= relevantTo)
            {
                throw new ValidationException($"Private lesson must have a relevent-to date greater than created date.");
            }
            RelevantTo = relevantTo;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetTime(double time)
        {
            if(time == 0.0)
            {
                throw new ValidationException($"Time cannot be null or 0.");
            }
            Time = time;
        }

        public void AssignTutor(User tutor, double pricePerHour)
        {
            if (IsAssigned)
            {
                throw new ValidationException($"Private lesson was already taken by another tutor at: {TakenAt}.");
            }
            if(pricePerHour == 0.0)
            {
                throw new ValidationException($"Price cannot be zero.");
            }
            PricePerHour = pricePerHour;
            TutorId = tutor.Id;
            TakenAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveAssignedTutor()
        {
            if (!IsAssigned)
            {
                throw new ValidationException($"You cannot untake an private lesson that was not taken before.");
            }
            PricePerHour = 0;
            TutorId = null;
            TakenAt = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeStatusToPaid()
        {
            IsPaid = true;
        }
    }
}
