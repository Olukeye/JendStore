using JendStore.Client.Models;
using JendStore.Client.Service.IService;
using JendStore.Client.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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

            if (response != null && response.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
                //_tokenProvider.SetToken(loginResponseDto.Token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomeError", response.Message);
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
                if (string.IsNullOrEmpty(registerDto.Role))
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

            var role = new List<SelectListItem>()
            {
                new SelectListItem{Text= HttpVerbs.RoleAdmin, Value= HttpVerbs.RoleAdmin },
                new SelectListItem{Text = HttpVerbs.RoleUser, Value = HttpVerbs.RoleUser }
            };

            ViewBag.RoleList = role;
            return View(registerDto);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}