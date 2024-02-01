using JendStore.Security.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JendStore.Security.Service.API.AuthRepository.JwtAction
{
    public class JwtToken : IJwtToken
    {
        private readonly JwtOptions _jwtOption;

        public JwtToken(IOptions<JwtOptions> jwtOption)
        {
            _jwtOption = jwtOption.Value;
        }


        public string TokenGenerator(ApiUser apiUser, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Environment.GetEnvironmentVariable("KEY");
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtOption.ExpiredTime));

            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, apiUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, apiUser.Email),
                new Claim(JwtRegisteredClaimNames.Name, apiUser.UserName)
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOption.Audience,
                Issuer = _jwtOption.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
