using System;

namespace Find_A_Tutor.Infrastructure.Commands.SchoolSubject
{
    public class UpdateSchoolSubject
    {
        public Guid SchoolSubjectId { get; set; }
        public string Name { get; set; }
    }
}
