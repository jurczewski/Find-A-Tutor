using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Repositories
{
    public class PrivateLessonRepository : IPrivateLessonRepository
    {
        private static readonly ISet<PrivateLesson> _privateLessons = new HashSet<PrivateLesson>()
        {
            new PrivateLesson(new Guid(), new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), DateTime.UtcNow.AddDays(14), "Pilnie potrzebne korepetycje z szeregów. Poziom studiów.", SchoolSubject.Mathematics),
            new PrivateLesson(new Guid(), new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), DateTime.UtcNow.AddDays(7), "Potrzebne pomoc z historią polski w wieku XVI", SchoolSubject.History),
            new PrivateLesson(new Guid(), new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), DateTime.UtcNow.AddDays(3), "Przygotowanie do matury - chemia", SchoolSubject.Chemistry)
    };

        public async Task<PrivateLesson> GetAsync(Guid id)
            => await Task.FromResult(_privateLessons.SingleOrDefault(x => x.Id == id));

        public async Task<PrivateLesson> GetAsyncBySubject(SchoolSubject name)
            => await Task.FromResult(_privateLessons.SingleOrDefault(x => x.Subject == name));

        public async Task<IEnumerable<PrivateLesson>> BrowseAsync(string description = "")
        {
            var privateLessons = _privateLessons.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(description))
            {
                privateLessons = _privateLessons.Where(x => x.Description.ToLowerInvariant()
                    .Contains(description.ToLowerInvariant()));
            }

            return await Task.FromResult(privateLessons);
        }

        public async Task AddAsync(PrivateLesson privateLesson)
        {
            _privateLessons.Add(privateLesson);
            await Task.CompletedTask;
        }
        public async Task UpdateAsync(PrivateLesson privateLesson)
        {
            //todo: Update method
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(PrivateLesson privateLesson)
        {
            _privateLessons.Remove(privateLesson);
            await Task.CompletedTask;
        }
    }
}
