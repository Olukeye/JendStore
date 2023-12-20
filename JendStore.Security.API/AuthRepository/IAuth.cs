using JendStore.Security.Service.API.DTO;

namespace JendStore.Security.Service.API.Repository
{
    public interface IAuth
    {
        Task<bool> ValidateUser(LoginDTO loginDTO);
        Task<string> CreateToken();
    }
}
