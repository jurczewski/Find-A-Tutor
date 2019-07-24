using Find_A_Tutor.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public interface ISchoolSubjectService
    {
        Task<SchoolSubject> GetAsync(Guid id);
        Task<SchoolSubject> GetAsync(string name);
        Task<IEnumerable<SchoolSubject>> BrowseAsync(string name = "");
        Task CreateAsync(Guid id, string name);
        Task UpdateAsync(Guid id, string name);
        Task DeleteAsync(Guid id);
    }
}
