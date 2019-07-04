using AutoMapper;
using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Infrastructure.DTO;

namespace Find_A_Tutor.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PrivateLesson, PrivateLessonDTO>();
                cfg.CreateMap<User, AccountDto>();
            })
            .CreateMapper();
    }
}
