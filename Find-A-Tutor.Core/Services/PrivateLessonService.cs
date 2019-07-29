using AutoMapper;
using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.DTO;
using Find_A_Tutor.Core.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public class PrivateLessonService : IPrivateLessonService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IPrivateLessonRepository _privateLessonRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISchoolSubjectRepository _schoolSubjectRepository;
        private readonly IMapper _mapper;

        public PrivateLessonService(IPrivateLessonRepository privateLessonRepository, IUserRepository userRepository, ISchoolSubjectRepository schoolSubjectRepository, IMapper mapper)
        {
            _privateLessonRepository = privateLessonRepository;
            _userRepository = userRepository;
            _schoolSubjectRepository = schoolSubjectRepository;
            _mapper = mapper;
        }

        public async Task<Result<PrivateLessonDTO>> GetAsync(Guid id)
        {
            logger.Info($"Fetching private lessons with id: '{id}'");
            var privateLesson = await _privateLessonRepository.GetAsync(id);

            return privateLesson != null ?
                                    Result<PrivateLessonDTO>.Ok(_mapper.Map<PrivateLessonDTO>(privateLesson)) :
                                    Result<PrivateLessonDTO>.Error($"Private lesson with id: '{id}', does not exists.");
        }

        public async Task<Result<IEnumerable<PrivateLessonDTO>>> GetAsyncBySubject(string subject)
        {
            logger.Info($"Fetching private lessons with subject: {subject}");
            var privateLesson = await _privateLessonRepository.GetAsyncBySubject(subject);
            return privateLesson != null ?
                                    Result<IEnumerable<PrivateLessonDTO>>.Ok(_mapper.Map<IEnumerable<PrivateLessonDTO>>(privateLesson)) :
                                    Result<IEnumerable<PrivateLessonDTO>>.Error($"There are no private lessons that subject contains \"{subject}\".");
        }

        public async Task<Result<IEnumerable<PrivateLessonDTO>>> GetForUserAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                return Result<IEnumerable<PrivateLessonDTO>>.Error($"User with id: '{userId}' does not exist.");
            }
            var allLessons = await _privateLessonRepository.BrowseAsync();

            var allLessonsForUser = _mapper.Map<IEnumerable<PrivateLessonDTO>>(allLessons.Where(x => x.StudentId == userId || x.TutorId == userId));

            return Result<IEnumerable<PrivateLessonDTO>>.Ok(allLessonsForUser);
        }

        public async Task<Result<IEnumerable<PrivateLessonDTO>>> BrowseAsync(string description = "")
        {
            logger.Info("Fetching private lessons");
            var privateLesson = await _privateLessonRepository.BrowseAsync(description);
            return Result<IEnumerable<PrivateLessonDTO>>.Ok(_mapper.Map<IEnumerable<PrivateLessonDTO>>(privateLesson));
        }

        public async Task<Result> CreateAsync(Guid id, Guid studnetId, DateTime relevantTo, string description, string subject)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(id);
            if (privateLesson != null)
            {
                return Result.Error("Private lesson already exists.");
            }

            var schoolSubject = await _schoolSubjectRepository.GetAsync(subject);
            if (schoolSubject == null)
            {
                return Result.Error($"School subject with name: '{subject}' does not exist.");
            }

            privateLesson = new PrivateLesson(id, studnetId, relevantTo, description, schoolSubject);
            await _privateLessonRepository.AddAsync(privateLesson);

            logger.Info($"Private lesson with id: '{id}', was successfully created");
            return Result.Ok();
        }

        public async Task<Result> UpdateAsync(Guid privateLessonId, DateTime relevantTo, string description, string subject)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(privateLessonId);
            if (privateLesson == null)
            {
                return Result.Error($"Private lesson with id: '{privateLessonId}' does not exist.");
            }

            var schoolSubject = await _schoolSubjectRepository.GetAsync(subject);
            if (schoolSubject == null)
            {
                return Result.Error($"Private lesson with name: '{subject}' does not exist.");
            }

            privateLesson.SetDesctiption(description);
            privateLesson.SetRelevantToDate(relevantTo);
            privateLesson.SetSchoolSubject(schoolSubject);

            await _privateLessonRepository.UpdateAsync(privateLesson);

            logger.Info($"Private lesson with id: '{privateLessonId}', was successfully updated");
            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(Guid privateLessonId)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(privateLessonId);
            if (privateLesson == null)
            {
                return Result.Error($"Private lesson with id: '{privateLessonId}' does not exist.");
            }
            await _privateLessonRepository.DeleteAsync(privateLesson);

            logger.Info($"Private lesson with id: '{privateLessonId}', was successfully deleted");
            return Result.Ok();
        }

        public async Task<Result> AssignTutor(Guid privateLessonId, Guid tutorId)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(privateLessonId);
            if (privateLesson == null)
            {
                return Result.Error($"Private lesson with id: '{privateLessonId}' does not exist.");
            }

            if (privateLesson.TutorId != null)
            {
                return Result.Error($"Private lesson is already assigned.");
            }
            var tutor = await _userRepository.GetAsync(tutorId);
            if (tutor == null)
            {
                return Result.Error($"User with id: '{tutorId}' does not exist.");
            }

            privateLesson.AssignTutor(tutor);

            logger.Info($"Tutor with id '{tutorId}' was assigned to lesson with id '{privateLessonId}'");
            return Result.Ok();
        }

        public async Task<Result> RemoveAssignedTutor(Guid privateLessonId, Guid userId)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(privateLessonId);
            if (privateLesson == null)
            {
                return Result.Error($"Private lesson with id: '{privateLessonId}' does not exist.");
            }

            if (privateLesson.TutorId != userId && privateLesson.StudentId != userId)
            {
                return Result.Error("Tutor can be unassign only by orignal student or tutor.");
            }
            privateLesson.RemoveAssignedTutor();

            logger.Info($"Assigned tutor was removed from lesson with id '{privateLessonId}'");
            return Result.Ok();
        }
    }
}
