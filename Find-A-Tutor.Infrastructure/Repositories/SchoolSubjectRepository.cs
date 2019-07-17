using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Repositories;
using Find_A_Tutor.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Repositories
{
    public class SchoolSubjectRepository : ISchoolSubjectRepository
    {
        private readonly FindATurorContext _context;
        public SchoolSubjectRepository(FindATurorContext Context)
        {
            _context = Context;
        }
        public async Task<SchoolSubject> GetAsync(Guid id)
            => await _context.SchoolSubjects.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<SchoolSubject> GetAsync(string name)
            => await _context.SchoolSubjects.SingleOrDefaultAsync(x => x.Name == name);

        public async Task<IEnumerable<SchoolSubject>> BrowseAsync(string name = "")
        {
            var schoolSubjects = _context.SchoolSubjects.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                schoolSubjects = schoolSubjects.Where(x => x.Name.ToLowerInvariant()
                    .Contains(name.ToLowerInvariant()));
            }
            return await Task.FromResult(schoolSubjects);
        }

        public async Task AddAsync(SchoolSubject schoolSubject)
        {
            await _context.SchoolSubjects.AddAsync(schoolSubject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SchoolSubject schoolSubject)
        {
            _context.SchoolSubjects.Update(schoolSubject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SchoolSubject schoolSubject)
        {
            _context.SchoolSubjects.Remove(schoolSubject);
            await _context.SaveChangesAsync();
        }
    }
}
