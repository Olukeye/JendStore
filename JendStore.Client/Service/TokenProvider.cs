using JendStore.Client.Service.IService;
using JendStore.Client.Utilities;

namespace JendStore.Client.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;

        public TokenProvider(IHttpContextAccessor httpcontextAccessor)
        {
            _httpcontextAccessor = httpcontextAccessor;
        }

        public void ClearToken()
        {
            _httpcontextAccessor.HttpContext?.Response.Cookies.Delete(HttpVerbs.TokenCookie);
        }


        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _httpcontextAccessor.HttpContext?.Request.Cookies.TryGetValue(HttpVerbs.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _httpcontextAccessor.HttpContext?.Response.Cookies.Append(HttpVerbs.TokenCookie, token);
        }
    }
}
