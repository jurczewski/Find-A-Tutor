using Find_A_Tutor.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
