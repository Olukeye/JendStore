﻿using JendStore.Security.API.Data;
using JendStore.Security.API.Models;
using JendStore.Security.Service.API.AuthRepository.JwtAction;
using JendStore.Security.Service.API.DTO;
using Microsoft.AspNetCore.Identity;


namespace JendStore.Security.Service.API.AuthRepository
{
    public class Auth : IAuth
    {
        private readonly DatabaseContext _db;
        private readonly UserManager<ApiUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtToken _jwtToken;


        public Auth(UserManager<ApiUser> userManager, DatabaseContext db, RoleManager<IdentityRole> roleManager, IJwtToken jwtToken)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtToken = jwtToken;

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
                        Address = returnUser.Address
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

        public async Task<LoginResponseDto> Login(LoginRequestDto loginDto)
        {
            var user = _db.ApiUser.FirstOrDefault(u => u.UserName.ToLower() == loginDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = ""};
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtToken.TokenGenerator(user, roles);

            UserDto userDto = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApiUser.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //Add role
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }
    }
}
