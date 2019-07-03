using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly ISet<User> _users = new HashSet<User>()
        {
            new User(new Guid("57b9e370-e6ae-47fc-992d-0bf488f75957"), "student", "Jan", "Kowalski", "jan.kowalski@gmail.com", "a12345678"),
            new User(new Guid("41e43b3a-9999-403d-ae14-a34b0c95853a"), "tutor", "Jakub", "Nowak", "jakub.nowak@gmail.com", "a12345678"),
            new User(new Guid("fac56674-0a27-479e-a372-abce89b38a48"), "admin", "Michał", "Wójcik", "michal.wojcik@gmail.com", "a12345678")
        };

        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant()));

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }
    }
}
