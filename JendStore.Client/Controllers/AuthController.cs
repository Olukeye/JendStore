using JendStore.Client.Models;
using JendStore.Client.Service;
using Microsoft.AspNetCore.Mvc;

namespace JendStore.Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService  authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
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
