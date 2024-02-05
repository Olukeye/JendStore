using JendStore.Client.Models;
using JendStore.Client.Service.IService;
using JendStore.Client.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace JendStore.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AccountController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginDto loginDto = new();

            return View(loginDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            ResponsDto response = await _authService.LoginAsync(loginDto);

            if (response!= null && response.IsSuccess)
            {
                LoginResponseDto model = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
                //await StaySignedIn(model);
                _tokenProvider.SetToken(model.Token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["success"] = response.Message;
                return View(loginDto);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var role = new List<SelectListItem>()
            {
                new SelectListItem{Text= HttpVerbs.RoleAdmin, Value= HttpVerbs.RoleAdmin },
                new SelectListItem{ Text = HttpVerbs.RoleUser, Value = HttpVerbs.RoleUser }
            };
            ViewBag.RoleList = role;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            ResponsDto result = await _authService.RegisterAsync(registerDto);
            ResponsDto assignRole;

            if(result!=null && result.IsSuccess)
            {
                if(string.IsNullOrEmpty(registerDto.Role))
                {
                    registerDto.Role = HttpVerbs.RoleUser;
                }

                assignRole = await _authService.AssignRoleAsync(registerDto);
                if(assignRole!=null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["success"] = result.Message;
            }

            var role = new List<SelectListItem>()
            {
                new SelectListItem{Text= HttpVerbs.RoleAdmin, Value= HttpVerbs.RoleAdmin },
                new SelectListItem{Text = HttpVerbs.RoleUser, Value = HttpVerbs.RoleUser }
            };

            ViewBag.RoleList = role;
            return View(registerDto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task StaySignedIn(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity();

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            //identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}