using AutoMapper;
using JendStore.Security.API.Models;
using JendStore.Security.Service.API.DTO;
using JendStore.Security.Service.API.AuthRepository;
using JendStore.Security.Service.API.ResponseHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JendStore.Security.Service.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        protected ResponseStatus _response;
        private readonly IAuth _auth;

        public AccountController(UserManager<ApiUser> userManager, IMapper mapper, ILogger<AccountController> logger, IAuth auth)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _auth = auth;
            _response = new();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration attempt for {userDTO.Email}!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<ApiUser>(userDTO);
            user.UserName = userDTO.Email;
            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (!result.Succeeded)
            {
                //Give exact Error message info on the action performed 
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userDTO.Roles);

            return Accepted(_response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            _logger.LogInformation($"Login attempt for {loginDto.Email}!");
            if (!ModelState.IsValid)
            {
                _response.Result = ModelState;
            }

            if (!await _auth.ValidateUser(loginDto))
            {
                return Unauthorized(_response);
            }

            return Accepted(new { Token = await _auth.CreateToken()});
        }

        //[HttpPost]
        //[Route("AssignRole")]
        //public async Task<IActionResult> AssignRole([FromBody] UserDTO userDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(userDto.Email.ToLower());

        //    if (user == null )
        //    {
        //        _response.Success = false;
        //        _response.Message = "Error ecountered";
        //        return BadRequest(_response);

        //    }
        //    await _userManager.AddToRolesAsync(user, userDto.Roles);
        //    return Ok(_response);
        //}
    }
}