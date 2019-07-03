using AutoMapper;
using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Repositories;
using Find_A_Tutor.Infrastructure.DTO;
using Find_A_Tutor.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Services
{
    public class PrivateLessonService : IPrivateLessonService
    {
        private readonly IPrivateLessonRepository _privateLessonRepository;
        private readonly IMapper _mapper;

        public PrivateLessonService(IPrivateLessonRepository privateLessonRepository, IMapper mapper)
        {
            _privateLessonRepository = privateLessonRepository;
            _mapper = mapper;
        }

        public async Task<PrivateLessonDTO> GetAsync(Guid id)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(id);
            return _mapper.Map<PrivateLessonDTO>(privateLesson);
        }

        public async Task<PrivateLessonDTO> GetAsyncBySubject(SchoolSubjectDTO name)
        {
            var privateLesson = await _privateLessonRepository.GetAsyncBySubject((SchoolSubject)name);
            return _mapper.Map<PrivateLessonDTO>(privateLesson);
        }

        public async Task<IEnumerable<PrivateLessonDTO>> BrowseAsync(string description = "")
        {
            var privateLesson = await _privateLessonRepository.BrowseAsync(description);
            return _mapper.Map<IEnumerable<PrivateLessonDTO>>(privateLesson);
        }
        public async Task CreateAsync(Guid id, Guid studnetId, DateTime relevantTo, string description, SchoolSubjectDTO subject)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(id);
            if (privateLesson != null)
            {
                throw new Exception($"Private lesson already exists.");
            }

            privateLesson = new PrivateLesson(id, studnetId, relevantTo, description, (SchoolSubject)subject);
            await _privateLessonRepository.AddAsync(privateLesson);
        }
        public async Task UpdateAsync(Guid id, string description)
        {
            var privateLesson = await _privateLessonRepository.GetAsync(id);
            if (privateLesson != null)
            {
                throw new Exception($"Private lesson already exists.");
            }
            privateLesson = await _privateLessonRepository.GetOrFailAsync(id);

            privateLesson.SetDesctiption(description);
            await _privateLessonRepository.UpdateAsync(privateLesson);
        }
        public async Task DeleteAsync(Guid id)
        {
            var privateLesson = await _privateLessonRepository.GetOrFailAsync(id);
            await _privateLessonRepository.DeleteAsync(privateLesson);
        }
        public async Task AssignTutor(Guid id, Guid tutor)
        {
            //var privateLesson = await _privateLessonRepository.GetOrFailAsync(id);
            //var tutor = await _userRepository.GetOrFailAsync(id);
            //privateLesson.Take(tutor);
            throw new NotImplementedException();
        }
        public async Task RemoveAssignedTutor(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
