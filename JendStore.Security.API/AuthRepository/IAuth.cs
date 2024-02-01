using JendStore.Security.Service.API.DTO;

namespace JendStore.Security.Service.API.AuthRepository
{
    public interface IAuth
    {
        Task<string> Register(RegistrationDto regDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
