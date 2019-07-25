using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.DTO;
using System;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Services
{
    public interface IUserService
    {
        Task<Result<AccountDto>> GetAccountAsync(Guid user);
        Task<Result> RegisterAsync(Guid userId, string email, string firstName, string lastName, string password, string role = "student");
        Task<Result<TokenDto>> LoginAsync(string email, string password);
    }
}
