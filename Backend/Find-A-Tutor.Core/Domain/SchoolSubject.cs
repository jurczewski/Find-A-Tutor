using Find_A_Tutor.Core.Exceptions;
using System;

namespace Find_A_Tutor.Core.Domain
{
    public class SchoolSubject : Entity
    {
        public string Name { get; protected set; }

        public SchoolSubject(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }

        public void SetName(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ValidationException($"School subject can not have an empty name.");
            }
            Name = subject;
        }
    }
}
