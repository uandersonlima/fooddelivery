using AutoMapper;

namespace fooddelivery.Models.DTO
{
    public class DTOMapperProfile : Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<User, RegisterUserDTO>();
            CreateMap<RegisterUserDTO, User>()
                    .ForMember(dest => dest.UserName, orig => orig.MapFrom(u => u.Email));
        }
    }
}