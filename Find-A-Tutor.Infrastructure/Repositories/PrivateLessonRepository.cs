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
    public class PrivateLessonRepository : IPrivateLessonRepository
    {
        private readonly FindATurorContext _context;
        public PrivateLessonRepository(FindATurorContext Context)
        {
            _context = Context;
        }
        public async Task<PrivateLesson> GetAsync(Guid id)
            => await _context.PrivateLessons.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<PrivateLesson> GetAsyncBySubject(string name)
            => await _context.PrivateLessons.SingleOrDefaultAsync(x => x.SchoolSubject.Name == name);

        public async Task<IEnumerable<PrivateLesson>> BrowseAsync(string description = "")
        {
            var privateLessons = _context.PrivateLessons.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(description))
            {
                privateLessons = privateLessons.Where(x => x.Description.ToLowerInvariant()
                    .Contains(description.ToLowerInvariant()));
            }
            return await Task.FromResult(privateLessons);
        }

        public async Task AddAsync(PrivateLesson privateLesson)
        {
            await _context.PrivateLessons.AddAsync(privateLesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PrivateLesson privateLesson)
        {
            _context.PrivateLessons.Update(privateLesson);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PrivateLesson privateLesson)
        {
            _context.PrivateLessons.Remove(privateLesson);
            await _context.SaveChangesAsync();
        }
    }
}
