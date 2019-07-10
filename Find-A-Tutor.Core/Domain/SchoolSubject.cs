using System;

namespace Find_A_Tutor.Core.Domain
{
    public class SchoolSubject
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public SchoolSubject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void SetSubject(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new Exception($"School subject can not have an empty name.");
            }
            Name = subject;
        }
    }
}
