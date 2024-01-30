using JendStore.Security.Service.API.DTO;

namespace JendStore.Security.Service.API.AuthRepository
{
    public interface IAuth2
    {
        Task<string> Register(RegistrationDto regDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginDto);
    }
}
