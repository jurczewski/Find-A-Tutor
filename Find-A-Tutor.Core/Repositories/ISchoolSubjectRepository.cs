using Find_A_Tutor.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Repositories
{
    public interface ISchoolSubjectRepository
    {
        Task<SchoolSubject> GetAsync(int id);
        Task<SchoolSubject> GetAsync(string name);
        Task<IEnumerable<SchoolSubject>> BrowseAsync(string name = "");
        Task AddAsync(SchoolSubject schoolSubject);
        Task UpdateAsync(SchoolSubject schoolSubject);
        Task DeleteAsync(SchoolSubject schoolSubject);
    }
}
