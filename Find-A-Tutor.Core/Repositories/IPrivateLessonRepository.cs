using Find_A_Tutor.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Repositories
{
    public interface IPrivateLessonRepository
    {
        Task<PrivateLesson> GetAsync(Guid id);
        Task<PrivateLesson> GetAsync(SchoolSubject name); //todo: am I sure?
        Task<IEnumerable<PrivateLesson>> BrowseAsync(string description = "");
        Task AddAsync(PrivateLesson privateLesson);
        Task UpdateAsync(PrivateLesson privateLesson);
        Task DeleteAsync(PrivateLesson privateLesson);
    }
}
