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

        public async Task<ResponseDTOStatus?> RegisterAsync(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDTOModel()
            {
                ApiType = HttpVerbs.ApiType.POST,
                Data = registerDto, 
                Url = HttpVerbs.AuthAPIBase + "/api/auth/Register"
            });
        }

        public async Task<ResponseDTOStatus?> LoginAsync(LoginDto loginDto)
        {
            return await _baseService.SendAsync(new RequestDTOModel()
            {
                ApiType = HttpVerbs.ApiType.POST,
                Data = loginDto,
                Url = HttpVerbs.AuthAPIBase + "/api/auth/Login"
            });
        }

        public async Task<ResponseDTOStatus?> AssignRoleAsync(RegisterDto registerDto)
        {

            return await _baseService.SendAsync(new RequestDTOModel()
            {
                ApiType = HttpVerbs.ApiType.POST,
                Data = registerDto,
                Url = HttpVerbs.AuthAPIBase + "/api/auth/AssignRole"
            });
        }
    }
}