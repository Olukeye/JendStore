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
                new SelectListItem{ Text = HttpVerbs.RoleCustomer, Value = HttpVerbs.RoleCustomer }
            };
            ViewBag.Role = role;

            return View();
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
