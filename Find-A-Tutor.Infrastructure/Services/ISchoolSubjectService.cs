using Find_A_Tutor.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Services
{
    public interface ISchoolSubjectService
    {
        Task<SchoolSubject> GetAsync(int id);
        Task<SchoolSubject> GetAsync(string name);
        Task<IEnumerable<SchoolSubject>> BrowseAsync(string name = "");
        Task CreateAsync(string name);
        Task UpdateAsync(int id, string name);
        Task DeleteAsync(int id);
    }
}
