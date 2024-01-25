using JendStore.Client.Models;

namespace JendStore.Client.Service.IService
{
    public interface IAuthService
    {
        Task<ResponsDto?> RegisterAsync(RegisterDto registerDto);
        Task<ResponsDto?> LoginAsync(LoginDto loginDto);
        Task<ResponsDto?> AssignRoleAsync(RegisterDto registerDto);

    }
}
