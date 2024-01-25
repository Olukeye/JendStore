using AutoMapper;
using JendStore.Security.API.Data;
using JendStore.Security.API.Models;
using JendStore.Security.Service.API.DTO;
using Microsoft.AspNetCore.Identity;



namespace JendStore.Security.Service.API.AuthRepository
{
    public class Auth2 : IAuth2
    {
        private readonly DatabaseContext _db;
        private readonly UserManager<ApiUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public Auth2(UserManager<ApiUser> userManager, DatabaseContext db, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }


        public Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegistrationDto regDto)
        {
            ApiUser user = new()
            {
                UserName = regDto.Email,
                FirstName = regDto.FirstName,
                LastName = regDto.LastName,
                Address = regDto.Address,
                Email = regDto.Email,
                NormalizedEmail = regDto.Email,
                PhoneNumber = regDto.PhoneNumber,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, regDto.Password);
                if (result.Succeeded)
                {
                    var returnUser = _db.ApiUser.FirstOrDefault(u => u.UserName == regDto.Email);

                    UserDto userDto = new()
                    {
                        FirstName = returnUser.FirstName,
                        LastName = returnUser.LastName,
                        Address = returnUser.Address,

                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description; 
                }
            }
            catch (Exception ex)
            {

            }
            return "Error!";
        }
    }
}
