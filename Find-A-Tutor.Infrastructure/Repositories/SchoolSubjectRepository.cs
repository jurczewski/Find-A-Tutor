using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Repositories
{
    public class SchoolSubjectRepository : ISchoolSubjectRepository
    {
        private static readonly ISet<SchoolSubject> _schoolSubjects = new HashSet<SchoolSubject>()
        {
            new SchoolSubject(new Guid("51f2080b-6685-44a6-8188-e4947ec103a8"), "Mathematics"),
            new SchoolSubject(Guid.NewGuid(), "Science"),
            new SchoolSubject(new Guid("5f26998e-5e9c-4224-9a37-484ef6e03d9b"), "Biology"),
            new SchoolSubject(Guid.NewGuid(), "Physics"),
            new SchoolSubject(new Guid("b14c0a4c-8f37-47ba-a63d-91dba7fedbe1"), "Chemistry"),
            new SchoolSubject(Guid.NewGuid(), "Geography"),
            new SchoolSubject(new Guid("d917539f-5fa3-4457-b785-a156155bfcbe"), "History"),
            new SchoolSubject(Guid.NewGuid(), "Citizenship"),
            new SchoolSubject(Guid.NewGuid(), "Art"),
            new SchoolSubject(Guid.NewGuid(), "Music"),
            new SchoolSubject(Guid.NewGuid(), "Polish"),
            new SchoolSubject(Guid.NewGuid(), "English"),
            new SchoolSubject(Guid.NewGuid(), "English"),
            new SchoolSubject(Guid.NewGuid(), "French"),
            new SchoolSubject(Guid.NewGuid(), "German")
        };

        public async Task<SchoolSubject> GetAsync(Guid id)
            => await Task.FromResult(_schoolSubjects.SingleOrDefault(x => x.Id == id));

        public async Task<SchoolSubject> GetAsync(string name)
            => await Task.FromResult(_schoolSubjects.SingleOrDefault(x => x.Name == name));

        public async Task<IEnumerable<SchoolSubject>> BrowseAsync(string name = "")
        {
            var schoolSubjects = _schoolSubjects.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                schoolSubjects = _schoolSubjects.Where(x => x.Name.ToLowerInvariant()
                    .Contains(name.ToLowerInvariant()));
            }

            return await Task.FromResult(schoolSubjects);
        }

        public async Task AddAsync(SchoolSubject schoolSubject)
        {
            _schoolSubjects.Add(schoolSubject);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(SchoolSubject schoolSubject)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(SchoolSubject schoolSubject)
        {
            _schoolSubjects.Remove(schoolSubject);
            await Task.CompletedTask;
        }
    }
}
