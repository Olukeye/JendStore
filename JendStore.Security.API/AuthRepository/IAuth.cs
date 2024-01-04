using JendStore.Security.Service.API.DTO;

namespace JendStore.Security.Service.API.AuthRepository
{
    public interface IAuth
    {
        Task<bool> ValidateUser(LoginDTO loginDTO);
        Task<string> CreateToken();
        //Task<bool> AssignRole(string roleName, string email);
    }
}
