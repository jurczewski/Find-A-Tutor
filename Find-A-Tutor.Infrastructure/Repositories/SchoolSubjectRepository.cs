using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Repositories
{
    public class SchoolSubjectRepository : ISchoolSubjectRepository
    {
        private static readonly ISet<SchoolSubject> _schoolSubjects = new HashSet<SchoolSubject>()
        {
            new SchoolSubject(1, "Mathematics"),
            new SchoolSubject(2, "Science"),
            new SchoolSubject(3, "Biology"),
            new SchoolSubject(4, "Physics"),
            new SchoolSubject(5, "Chemistry"),
            new SchoolSubject(6, "Geography"),
            new SchoolSubject(7, "History"),
            new SchoolSubject(8, "Citizenship"),
            new SchoolSubject(9, "Art"),
            new SchoolSubject(10, "Music"),
            new SchoolSubject(11, "Polish"),
            new SchoolSubject(12, "English"),
            new SchoolSubject(12, "English"),
            new SchoolSubject(14, "French"),
            new SchoolSubject(15, "German"),
        };

        public int NextId => _schoolSubjects.Count;

        public async Task<SchoolSubject> GetAsync(int id)
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
            schoolSubject.SetId(NextId);

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
