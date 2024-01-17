using JendStore.Client.Models;
using JendStore.Client.Service.IService;
using JendStore.Client.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JendStore.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
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
            ResponseDTOStatus result = await _authService.RegisterAsync(registerDto);
            ResponseDTOStatus assignRole;

            if(result!=null && result.Status)
            {
                if (string.IsNullOrEmpty(registerDto.Roles))
                {
                    registerDto.Roles = HttpVerbs.RoleUser;
                }

                assignRole = await _authService.AssignRoleAsync(registerDto);
                if(assignRole!=null && assignRole.Status)
                {
                    TempData["sucess"] = "Success";
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
        public IActionResult Login()
        {
            LoginDto loginDto = new();

            return View(loginDto);
        }


        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
