using Find_A_Tutor.Infrastructure.DTO;
using System;
using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAccountAsync(Guid user);
        Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user");
        Task<TokenDto> LoginAsync(string email, string password);
    }
}
