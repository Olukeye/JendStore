using System.ComponentModel.DataAnnotations;

namespace JendStore.Security.Service.API.DTO
{

    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(14, ErrorMessage = "Your Password Is Limited To {2} To {1} Characters", MinimumLength = 4)]
        public string Password { get; set; }
    }
    
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }

    public class RegistrationDto : LoginRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string? Role { get; set; }
    }

    public class UserDto 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } 
    }



    //public class UserResponse
    //{
    //    public UserDto User { get; set; }
    //    public string Token { get; set; }
    //}
}