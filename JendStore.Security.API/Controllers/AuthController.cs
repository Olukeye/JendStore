using AutoMapper;
using Azure;
using JendStore.Security.API.Models;
using JendStore.Security.Service.API.AuthRepository;
using JendStore.Security.Service.API.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JendStore.Security.Service.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuth _auth;
        protected ResponsDto _responseDto;

        public AuthController(UserManager<ApiUser> userManager, ILogger<AuthController> logger, IMapper mapper, IAuth auth)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _responseDto = new();
            _auth = auth;

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto regDto)
        {
            _logger.LogInformation($"Registration attempt for {regDto.Email}!");
            var error = await _auth.Register(regDto);
            if (!string.IsNullOrEmpty(error))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ($"You Encountered: {error}");
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            _logger.LogInformation($"Registration attempt for {loginDto}!");
            var loginResponse = await _auth.Login(loginDto);
            if (loginResponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ($"Incorrect Credentials");
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginResponse;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] UserDto user)
        {
            var userRole = await _auth.AssignRole(user.Email, user.Role.ToUpper());
            if (!userRole)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error ecountered";
                return BadRequest();
            }
            return Ok(_responseDto);
        }
    }
}
