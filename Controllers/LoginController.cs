using Microsoft.AspNetCore.Mvc;
using PROG7312_P1_V1.DataModels;
using PROG7312_P1_V1.Languages;
using PROG7312_P1_V1.Services;

namespace PROG7312_P1_V1.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        // Checks is login details are correct 
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _loginService.AuthenticateUser(email, password);

            if (user != null)
            {
                // User authenticated successfully, redirect to a secure area
                return RedirectToAction("MainView", "MainView");
            }

            // Lofin failed, return to login view with error
            ViewBag.ErrorLogin = $"{Labels.txtErrorLogin}";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(string fullName, string email, string password, string phoneNumber)
        {
            // checks if the email already exists
            if (await _loginService.ProfileExists(email))
            {
                ViewBag.ErrorEmailRegister = $"{Labels.txtErrorEmailRegister}";
                return View();
            }

            await _loginService.RegisterUser(new DataModels.Profile
            {
                FullName = fullName,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber
            });

            TempData["SignupMessage"] = "Signup Successful -- Please Login";
            return RedirectToAction("Login");
        }
    }
}
