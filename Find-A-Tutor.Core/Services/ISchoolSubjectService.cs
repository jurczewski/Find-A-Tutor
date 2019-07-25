using Find_A_Tutor.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public interface ISchoolSubjectService
    {
        Task<Result<SchoolSubject>> GetAsync(Guid id);
        Task<Result<SchoolSubject>> GetAsync(string name);
        Task<Result<IEnumerable<SchoolSubject>>> BrowseAsync(string name = "");
        Task<Result> CreateAsync(Guid id, string name);
        Task<Result> UpdateAsync(Guid id, string name);
        Task<Result> DeleteAsync(Guid id);
    }
}
