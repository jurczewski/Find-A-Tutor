using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Repositories
{
    public class InMemoryPrivateLessonRepository : IPrivateLessonRepository
    {
        private static readonly ISet<PrivateLesson> _privateLessons = new HashSet<PrivateLesson>()
        {
            new PrivateLesson(new Guid("cdb68f36-1590-4667-a1fb-7977fd31fc70"), new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), DateTime.UtcNow.AddDays(14), "Pilnie potrzebne korepetycje z szeregów. Poziom studiów.", new SchoolSubject(new Guid("51f2080b-6685-44a6-8188-e4947ec103a8"), "Mathematics")),
            new PrivateLesson(Guid.NewGuid(), new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), DateTime.UtcNow.AddDays(7), "Potrzebne pomoc z historią polski w wieku XVI", new SchoolSubject(new Guid("d917539f-5fa3-4457-b785-a156155bfcbe"), "History")),
            new PrivateLesson(Guid.NewGuid(), new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), DateTime.UtcNow.AddDays(3), "Przygotowanie do matury - chemia", new SchoolSubject(new Guid("b14c0a4c-8f37-47ba-a63d-91dba7fedbe1"), "Chemistry"))
        };

        public async Task<PrivateLesson> GetAsync(Guid id)
            => await Task.FromResult(_privateLessons.SingleOrDefault(x => x.Id == id));

        public async Task<PrivateLesson> GetAsyncBySubject(string name)
            => await Task.FromResult(_privateLessons.SingleOrDefault(x => x.SchoolSubject.Name == name));

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
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(PrivateLesson privateLesson)
        {
            _privateLessons.Remove(privateLesson);
            await Task.CompletedTask;
        }
    }
}
