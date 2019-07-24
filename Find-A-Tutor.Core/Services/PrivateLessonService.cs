using AutoMapper;
using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.DTO;
using Find_A_Tutor.Core.Exceptions;
using Find_A_Tutor.Core.Extensions;
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
            var privateLesson = await _privateLessonRepository.GetAsync(id);

            return privateLesson != null ?
                                    Result<PrivateLessonDTO>.Ok(_mapper.Map<PrivateLessonDTO>(privateLesson)) :
                                    Result<PrivateLessonDTO>.Error($"Private lesson with id: {id}, does not exists.");
        }

        public async Task<PrivateLessonDTO> GetAsyncBySubject(string name)
        {
            var privateLesson = await _privateLessonRepository.GetAsyncBySubject(name);
            return _mapper.Map<PrivateLessonDTO>(privateLesson);
        }

        public async Task<IEnumerable<PrivateLessonDTO>> GetForUserAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var allLessons = await _privateLessonRepository.BrowseAsync();

            var allLessonsForUser = new List<PrivateLessonDTO>();

            allLessonsForUser.AddRange(_mapper.Map<IEnumerable<PrivateLessonDTO>>(allLessons.Where(x => x.StudentId == userId || x.TutorId == userId)));

            return allLessonsForUser;
        }

        public async Task<IEnumerable<PrivateLessonDTO>> BrowseAsync(string description = "")
        {
            logger.Info("Fetching events");
            var privateLesson = await _privateLessonRepository.BrowseAsync(description);
            return _mapper.Map<IEnumerable<PrivateLessonDTO>>(privateLesson);
        }

        public async Task CreateAsync(Guid id, Guid studnetId, DateTime relevantTo, string description, string subject)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(id);
            if (privateLesson != null)
            {
                throw new RepositoryException($"Private lesson already exists.");
            }

            var schoolSubject = await _schoolSubjectRepository.GetOrFailAsync(subject);

            privateLesson = new PrivateLesson(id, studnetId, relevantTo, description, schoolSubject);
            await _privateLessonRepository.AddAsync(privateLesson);

            logger.Info($"Private lesson with id: {0}, was successfully created", id);
        }

        public async Task UpdateAsync(Guid privateLessonId, DateTime relevantTo, string description, string subject)
        {
            var privateLesson = await _privateLessonRepository.GetOrFailAsync(privateLessonId);
            var schoolSubject = await _schoolSubjectRepository.GetOrFailAsync(subject);

            privateLesson.SetDesctiption(description);
            privateLesson.SetRelevantToDate(relevantTo);
            privateLesson.SetSchoolSubject(schoolSubject);

            await _privateLessonRepository.UpdateAsync(privateLesson);

            logger.Info($"Private lesson with id: {0}, was successfully updated", privateLessonId);
        }

        public async Task DeleteAsync(Guid privateLessonId)
        {
            var privateLesson = await _privateLessonRepository.GetOrFailAsync(privateLessonId);
            await _privateLessonRepository.DeleteAsync(privateLesson);

            logger.Info($"Private lesson with id: {0}, was successfully deleted", privateLessonId);
        }

        public async Task AssignTutor(Guid privateLessonId, Guid tutorId)
        {
            var privateLesson = await _privateLessonRepository.GetOrFailAsync(privateLessonId);
            if (privateLesson.TutorId != null)
            {
                throw new RepositoryException($"Private lesson is already assigned.");
            }
            var tutor = await _userRepository.GetOrFailAsync(tutorId);
            privateLesson.AssignTutor(tutor);

            logger.Info($"Tutor with id {0} was assigned to lesson with id {1}", tutorId, privateLessonId);
        }

        public async Task RemoveAssignedTutor(Guid privateLessonId, Guid userId)
        {
            var privateLesson = await _privateLessonRepository.GetOrFailAsync(privateLessonId);
            if (privateLesson.TutorId != userId && privateLesson.StudentId != userId)
            {
                throw new RepositoryException("Tutor can be unassign only by orignal student or tutor.");
            }
            privateLesson.RemoveAssignedTutor();

            logger.Info($"Assigned tutor was removed from lesson with id {1}", privateLessonId);
        }
    }
}
