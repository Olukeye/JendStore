using JendStore.Security.API.Models;


namespace JendStore.Security.Service.API.AuthRepository.JwtAction
{
    public interface IJwtToken
    {
        string TokenGenerator(ApiUser apiUser, IEnumerable<string> roles);
    }
}
