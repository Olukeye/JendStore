//using JendStore.Security.API.Data;
//using JendStore.Security.API.Models;
//using JendStore.Security.Service.API.DTO;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace JendStore.Security.Service.API.AuthRepository
//{
//    public class Auth : IAuth
//    {
//        private readonly DatabaseContext _db;
//        private readonly UserManager<ApiUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly IConfiguration _configuration;
//        private ApiUser _user;


//        public Auth(UserManager<ApiUser> userManager, DatabaseContext db,  IConfiguration configuration, RoleManager<IdentityRole> roleManager)
//        {
//            _db = db;
//            _roleManager = roleManager;
//            _userManager = userManager;
//            _configuration = configuration;
//        }

//        public async Task<string> CreateToken()
//        {
//            var signingCredentials = GetSigningCredentials();
//            var claims = await GetClaims();
//            var token = GenerateTokenOptions(signingCredentials, claims);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

    
//        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
//        {
//            var jwtSettings = _configuration.GetSection("Jwt");
//            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("ExpiredTime").Value));

//            var token = new JwtSecurityToken(
//                issuer: jwtSettings.GetSection("ValidIssuer").Value,
//                claims: claims,
//                expires: expiration,
//                signingCredentials: signingCredentials
//                );

//            return token;
//        }

//        private async Task<List<Claim>> GetClaims()
//        {
//            var claims = new List<Claim>
//           {
//               new Claim(ClaimTypes.Name, _user.UserName),
//           };

//            var roles = await _userManager.GetRolesAsync(_user);
//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }
//            return claims;
//        }


//        private SigningCredentials GetSigningCredentials()
//        {
//            var key = Environment.GetEnvironmentVariable("KEY");
//            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

//            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
//        }

//        public async Task<bool> ValidateUser(LoginDTO loginDTO)
//        {
//            _user = await _userManager.FindByNameAsync(loginDTO.Email);
//            return (_user != null && await _userManager.CheckPasswordAsync(_user, loginDTO.Password));
//        }


//        public async Task<bool> AssignRole(string email, string roleName)
//        {
//            var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

//            if (user != null)
//            {
//                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
//                {
//                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
//                }

//                await _userManager.AddToRoleAsync(user, roleName);
//                return true;
//            }
//            return false;
//        }
//    }
//}
