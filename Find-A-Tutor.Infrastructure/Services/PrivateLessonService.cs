using AutoMapper;
using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Repositories;
using Find_A_Tutor.Infrastructure.DTO;
using Find_A_Tutor.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Services
{
    public class PrivateLessonService : IPrivateLessonService
    {
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

        public async Task<PrivateLessonDTO> GetAsync(Guid id)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(id);
            return _mapper.Map<PrivateLessonDTO>(privateLesson);
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

            allLessonsForUser.AddRange(_mapper.Map<IEnumerable<PrivateLessonDTO>>(allLessons.Where(x => x.StudnetId == userId || x.TutorId == userId)));

            return allLessonsForUser;
        }

        public async Task<IEnumerable<PrivateLessonDTO>> BrowseAsync(string description = "")
        {
            var privateLesson = await _privateLessonRepository.BrowseAsync(description);
            return _mapper.Map<IEnumerable<PrivateLessonDTO>>(privateLesson);
        }

        public async Task CreateAsync(Guid id, Guid studnetId, DateTime relevantTo, string description, string subject)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(id);
            if (privateLesson != null)
            {
                throw new Exception($"Private lesson already exists.");
            }

            var schoolSubject = await _schoolSubjectRepository.GetOrFailAsync(subject);

            privateLesson = new PrivateLesson(id, studnetId, relevantTo, description, schoolSubject);
            await _privateLessonRepository.AddAsync(privateLesson);
        }

        public async Task UpdateAsync(Guid privateLessonId, DateTime relevantTo, string description, string subject)
        {
            var privateLesson = await _privateLessonRepository.GetOrFailAsync(privateLessonId);
            var schoolSubject = await _schoolSubjectRepository.GetOrFailAsync(subject);

            privateLesson.SetDesctiption(description);
            privateLesson.SetRelevantToDate(relevantTo);
            privateLesson.SetSchoolSubject(schoolSubject);

            await _privateLessonRepository.UpdateAsync(privateLesson);
        }

        public async Task DeleteAsync(Guid privateLessonId)
        {
            var privateLesson = await _privateLessonRepository.GetOrFailAsync(privateLessonId);
            await _privateLessonRepository.DeleteAsync(privateLesson);
        }

        public async Task AssignTutor(Guid privateLessonId, Guid tutorId)
        {
            var privateLesson = await _privateLessonRepository.GetOrFailAsync(privateLessonId);
            if (privateLesson.TutorId != null)
            {
                throw new Exception($"Private lesson is already assigned.");
            }
            var tutor = await _userRepository.GetOrFailAsync(tutorId);
            privateLesson.AssignTutor(tutor);
        }

        public async Task RemoveAssignedTutor(Guid privateLessonId, Guid userId)
        {
            var privateLesson = await _privateLessonRepository.GetOrFailAsync(privateLessonId);
            if (privateLesson.TutorId != userId && privateLesson.StudnetId != userId)
            {
                throw new Exception("Tutor can be unassign only by orignal student or tutor.");
            }
            privateLesson.RemoveAssignedTutor();
        }
    }
}
