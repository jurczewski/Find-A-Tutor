using System;

namespace Find_A_Tutor.Core.Domain
{
    public class SchoolSubject
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public SchoolSubject(int id, string name)
        {
            SetId(id);
            SetName(name);
        }

        public void SetName(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new Exception($"School subject can not have an empty name.");
            }
            Name = subject;
        }

        public void SetId(int id)
        {
            if (id <= 0)
            {
                throw new Exception($"Id: {id} cannot be equal zero or below zero.");
            }
            Id = id;
        }
    }
}
