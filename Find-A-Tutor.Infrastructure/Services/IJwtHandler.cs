using Find_A_Tutor.Infrastructure.DTO;
using System;

namespace Find_A_Tutor.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}
