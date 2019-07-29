using Find_A_Tutor.Core.DTO;
using System;

namespace Find_A_Tutor.Core.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}
