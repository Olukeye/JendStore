namespace JendStore.Security.Service.API.AuthRepository.JwtAction
{
    public class JwtOptions
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public int ExpiredTime { get; init; }
    }
}
