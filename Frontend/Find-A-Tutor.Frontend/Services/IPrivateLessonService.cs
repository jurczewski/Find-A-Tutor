using Find_A_Tutor.Frontend.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public interface IPrivateLessonService
    {
        Task<Result<IEnumerable<PrivateLesson>>> GetAll();
        Task<Result<PrivateLesson>> Get(Guid privateLessonId);
        Task<Result> Post(PrivateLesson privateLesson);
        Task<Result> AssignTutor(string privateLessonId);
    }
}
