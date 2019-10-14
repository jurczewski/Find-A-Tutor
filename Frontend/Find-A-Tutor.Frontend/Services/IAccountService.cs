using Find_A_Tutor.Frontend.Model;
using Find_A_Tutor.Frontend.Model.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Find_A_Tutor.Frontend.Services
{
    public interface IAccountService
    {
        Task<Result<TokenDto>> Login(string email, string password);
        Task<Result<IEnumerable<PrivateLesson>>> GetLessonsForUser();
    }
}
