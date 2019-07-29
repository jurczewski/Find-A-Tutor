using System;

namespace Find_A_Tutor.Infrastructure.Commands.SchoolSubject
{
    public class CreateSchoolSubject
    {
        public Guid SchoolSubjectId { get; set; }
        public string Name { get; set; }
    }
}
