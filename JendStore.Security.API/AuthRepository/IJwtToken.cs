using JendStore.Security.API.Models;


namespace JendStore.Security.Service.API.AuthRepository
{
    public interface IJwtToken
    {
         string TokenGenerator(ApiUser apiUser);
    }
}
