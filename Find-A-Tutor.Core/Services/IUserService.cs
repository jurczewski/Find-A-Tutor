using Find_A_Tutor.Core.DTO;
using System;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAccountAsync(Guid user);
        Task RegisterAsync(Guid userId, string email, string firstName, string lastName, string password, string role = "student");
        Task<TokenDto> LoginAsync(string email, string password);
    }
}
