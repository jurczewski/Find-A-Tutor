using Find_A_Tutor.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public interface IPrivateLessonService
    {
        Task<PrivateLessonDTO> GetAsync(Guid id);
        Task<PrivateLessonDTO> GetAsyncBySubject(string name);
        Task<IEnumerable<PrivateLessonDTO>> GetForUserAsync(Guid userId);
        Task<IEnumerable<PrivateLessonDTO>> BrowseAsync(string description = "");
        Task CreateAsync(Guid privateLessonId, Guid studnetId, DateTime relevantTo, string description, string subject);
        Task UpdateAsync(Guid privateLessonId, DateTime relevantTo, string description, string subject);
        Task DeleteAsync(Guid privateLessonId);
        Task AssignTutor(Guid privateLessonId, Guid tutorId);
        Task RemoveAssignedTutor(Guid privateLessonId, Guid userId);
    }
}
