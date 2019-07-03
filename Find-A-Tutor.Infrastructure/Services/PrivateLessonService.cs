using AutoMapper;
using Find_A_Tutor.Core.Repositories;
using Find_A_Tutor.Infrastructure.DTO;
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
            //todo: mapper mess
            //var privateLesson = await _privateLessonRepository.GetAsyncBySubject((SchoolSubject)name);
            //return _mapper.Map<PrivateLessonDTO>(privateLesson);
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PrivateLessonDTO>> BrowseAsync(string description = "")
        {
            var privateLesson = await _privateLessonRepository.BrowseAsync(description);
            return _mapper.Map<IEnumerable<PrivateLessonDTO>>(privateLesson);
        }
        public async Task CreateAsync(Guid studnetId, string name, DateTime relevantTo, string description, SchoolSubjectDTO subject)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(Guid id, string description)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task Take(Guid id, Guid tutor)
        {
            throw new NotImplementedException();
        }
        public async Task Cancel(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
