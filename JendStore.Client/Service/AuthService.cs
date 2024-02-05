using JendStore.Client.Models;
using JendStore.Client.Service.IService;
using JendStore.Client.Sevice.IService;
using JendStore.Client.Utilities;
   

namespace JendStore.Client.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponsDto?> AssignRoleAsync(RegisterDto registerDto)
        {

            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.POST,
                Data = registerDto,
                Url = HttpVerbs.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponsDto?> RegisterAsync(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.POST,
                Data = registerDto, 
                Url = HttpVerbs.AuthAPIBase + "/api/auth/Register"
            }, bearer: false);
        }

        public async Task<ResponsDto?> LoginAsync(LoginDto loginDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = HttpVerbs.ApiType.POST,
                Data = loginDto,
                Url = HttpVerbs.AuthAPIBase + "/api/auth/Login"
            }, bearer: false);
        }
    }
}