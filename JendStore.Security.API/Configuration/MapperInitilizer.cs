using AutoMapper;
using JendStore.Security.API.Models;
using JendStore.Security.Service.API.DTO;


namespace JendStore.Security.Service.API.Configuration
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<ApiUser, UserDto>().ReverseMap();
            CreateMap<ApiUser, LoginDto>().ReverseMap();
        }
    }
}
