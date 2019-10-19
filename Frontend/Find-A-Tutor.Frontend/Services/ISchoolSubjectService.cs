using Find_A_Tutor.Frontend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public interface ISchoolSubjectService
    {
        Task<Result<IEnumerable<SchoolSubject>>> GetAll();
    }
}
