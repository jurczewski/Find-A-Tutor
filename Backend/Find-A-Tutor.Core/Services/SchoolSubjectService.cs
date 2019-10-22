using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public class SchoolSubjectService : ISchoolSubjectService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ISchoolSubjectRepository _schoolSubjectRepository;
        public SchoolSubjectService(ISchoolSubjectRepository schoolSubjectRepository)
        {
            _schoolSubjectRepository = schoolSubjectRepository;
        }

        public async Task<Result<SchoolSubject>> GetAsync(Guid id)
        {
            logger.Info($"Fetching school subjects with id: '{id}'");
            var schoolSubject = await _schoolSubjectRepository.GetAsync(id);
            return schoolSubject != null ?
                                    Result<SchoolSubject>.Ok(schoolSubject) :
                                    Result<SchoolSubject>.Error($"School subject with id: {id}, does not exists.");
        }

        public async Task<Result<SchoolSubject>> GetAsync(string name)
        {
            logger.Info($"Fetching school subjects with name: '{name}'");
            var schoolSubject = await _schoolSubjectRepository.GetAsync(name);
            return schoolSubject != null ?
                                    Result<SchoolSubject>.Ok(schoolSubject) :
                                    Result<SchoolSubject>.Error($"School subject with name: \"{name}\", does not exists.");
        }
        public async Task<Result<IEnumerable<SchoolSubject>>> BrowseAsync(string name = "")
        {
            logger.Info("Fetching school subjects");
            var schoolSubjects = await _schoolSubjectRepository.BrowseAsync(name);
            return Result<IEnumerable<SchoolSubject>>.Ok(schoolSubjects);
        }

        public async Task<Result> CreateAsync(Guid id, string name)
        {
            var schoolSubject = await _schoolSubjectRepository.GetAsync(name);
            if (schoolSubject != null)
            {
                return Result.Error($"School subject already exists.");
            }

            schoolSubject = new SchoolSubject(id, name);
            await _schoolSubjectRepository.AddAsync(schoolSubject);

            logger.Info($"School subject \"{name}\" with id: '{id}', was successfully created");
            return Result.Ok();
        }

        public async Task<Result> UpdateAsync(Guid id, string name)
        {
            var schoolSubjectById = await _schoolSubjectRepository.GetAsync(id);
            if (schoolSubjectById == null)
            {
                return Result.Error($"School subject with id: '{id}' does not exist.");
            }

            var schoolSubjectByName = await _schoolSubjectRepository.GetAsync(name);
            if (schoolSubjectByName != null)
            {
                return Result.Error("School subject with that name already exists.");
            }

            schoolSubjectById.SetName(name);

            await _schoolSubjectRepository.UpdateAsync(schoolSubjectById);
            logger.Info($"School subject with id: '{id}', was successfully updated");
            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var schoolSubjectById = await _schoolSubjectRepository.GetAsync(id);
            if (schoolSubjectById == null)
            {
                return Result.Error($"School subject with id: '{id}' does not exist.");
            }

            await _schoolSubjectRepository.DeleteAsync(schoolSubjectById);

            logger.Info($"School subject with id: '{id}', was successfully deleted");
            return Result.Ok();
        }
    }
}
