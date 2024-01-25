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
        private readonly IAuth2 _auth;
        protected ResponsDto _responseDto;

        public AuthController(UserManager<ApiUser> userManager, ILogger<AuthController> logger, IMapper mapper, IAuth2 auth)
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

    }
}
