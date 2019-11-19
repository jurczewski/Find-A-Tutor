using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public interface IPrivateLessonService
    {
        Task<Result<PrivateLessonDTO>> GetAsync(Guid id);
        Task<Result<IEnumerable<PrivateLessonDTO>>> GetAsyncBySubject(string name);
        Task<Result<IEnumerable<PrivateLessonDTO>>> GetForUserAsync(Guid userId);
        Task<Result<IEnumerable<PrivateLessonDTO>>> BrowseAsync(string description = "");
        Task<Result> CreateAsync(Guid privateLessonId, Guid studnetId, DateTime relevantTo, string description, string subject, double time);
        Task<Result> UpdateAsync(Guid privateLessonId, DateTime relevantTo, string description, string subject);
        Task<Result> DeleteAsync(Guid privateLessonId);
        Task<Result> AssignTutor(Guid privateLessonId, Guid tutorId, double pricePerHour);
        Task<Result> RemoveAssignedTutor(Guid privateLessonId, Guid userId);
        Task<Result> UpdatePaymentStatusToPaid(Guid privateLessonId, Guid userId);
    }
}
