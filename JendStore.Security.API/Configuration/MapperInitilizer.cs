using AutoMapper;
using JendStore.Security.API.Models;
using JendStore.Security.Service.API.DTO;


namespace JendStore.Security.Service.API.Configuration
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<ApiUser, UserDTO>().ReverseMap();
            CreateMap<ApiUser, LoginDTO>().ReverseMap();
        }
    }
}
