using Find_A_Tutor.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IPrivateLessonService _privateLessonService;
        public DataInitializer(IUserService userService, IPrivateLessonService privateLessonService)
        {
            _privateLessonService = privateLessonService;
            _userService = userService;
        }
        public async Task SeedAsync()
        {
            var tasks = new List<Task>();
            tasks.Add(_userService.RegisterAsync(new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), "student", "Jan", "Kowalski", "jan.kowalski@gmail.com", "a12345678"));
            tasks.Add(_userService.RegisterAsync(new Guid("41e43b3a-9999-403d-ae14-a34b0c95853a"), "tutor", "Jakub", "Nowak", "jakub.nowak@gmail.com", "a12345678"));
            tasks.Add(_userService.RegisterAsync(new Guid("fac56674-0a27-479e-a372-abce89b38a48"), "admin", "Michał", "Wójcik", "michal.wojcik@gmail.com", "a12345678"));

            tasks.Add(_privateLessonService.CreateAsync(new Guid(), new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), DateTime.UtcNow.AddDays(14), "Pilnie potrzebne korepetycje z szeregów. Poziom studiów.", SchoolSubjectDTO.Mathematics));
            tasks.Add(_privateLessonService.CreateAsync(new Guid(), new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), DateTime.UtcNow.AddDays(7), "Potrzebne pomoc z historią polski w wieku XVI", SchoolSubjectDTO.History));
            tasks.Add(_privateLessonService.CreateAsync(new Guid(), new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), DateTime.UtcNow.AddDays(3), "Przygotowanie do matury - chemia", SchoolSubjectDTO.Chemistry));

            await Task.WhenAll(tasks);
        }
    }
}
