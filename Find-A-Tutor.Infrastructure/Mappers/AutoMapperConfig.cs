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
                cfg.CreateMap<PrivateLesson, PrivateLessonDTO>()
                    .ForMember(x => x.Subject, m => m.MapFrom(p => p.SchoolSubject.Name));
                cfg.CreateMap<User, AccountDto>();
            })
            .CreateMapper();
    }
}
