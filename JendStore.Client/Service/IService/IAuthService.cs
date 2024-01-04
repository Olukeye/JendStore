using JendStore.Client.Models;

namespace JendStore.Client.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTOStatus?> RegisterAsync(RegisterDto registerDto);
        Task<ResponseDTOStatus?> LoginAsync(LoginDto loginDto);
    }
}
