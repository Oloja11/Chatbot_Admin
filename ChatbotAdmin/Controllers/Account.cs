using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ChatbotAdmin.Repository;
using ChatbotAdmin.Models;
using ChatbotAdmin.Services;
using Microsoft.Extensions.Logging;
using System.Text;
using ChatbotAdmin.Models.ViewModel;

namespace ChatbotAdmin.Controllers
{
    public class Account : Controller
    {
        private readonly ILoginManager loginManager;
        private readonly UserManagementService _userManagementService;
        private readonly ILogger<Account> _logger;
        private readonly List<string> sex = new() { "Male", "Female" };

        public Account(ILoginManager loginManager, UserManagementService userManagementService, ILogger<Account> logger)
        {
            this.loginManager = loginManager;
            _userManagementService = userManagementService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewUserDetails(ClubUser user)
        {
            if (user != null)
            {
                var clearPassword = Encoding.UTF8.GetString(Convert.FromBase64String(user.HashPassword));
                user.HashPassword = clearPassword;
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ViewBag.LoginError = "username and password required";
                return View();
            }
            ClaimsIdentity identity = null;
            var isAuthenticated = false;
            var hashPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            var clubUser = loginManager.Authenticate(userName, hashPassword);
            if (clubUser!=null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{clubUser.FirstName} {clubUser.LastName}"),
                    new Claim(ClaimTypes.Gender, clubUser.Sex),
                    new Claim(ClaimTypes.NameIdentifier,clubUser.Id.ToString()),
                    new Claim(ClaimTypes.Email, clubUser.Email),
                    new Claim(ClaimTypes.MobilePhone,clubUser.PhoneNumber)
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                clubUser.Role.ForEach(r => identity.AddClaim( new Claim(ClaimTypes.Role, r)));                
                
                isAuthenticated = true;
            }

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                TempData["Info"] = "Login Sei";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.LoginError = "username or password not correct";
            return View();
        }

        public IActionResult Register()
        {
            ViewBag.Sex = sex;
            return View();
        }

        [HttpPost]
        public IActionResult Register(ClubUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.HashPassword = user.FirstName;
                    var hasPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.HashPassword));
                    user.HashPassword = hasPassword;
                    user.IsActive = 1;
                    var newUser = _userManagementService.CreateUser(user);
                    return RedirectToAction(nameof(NewUserDetails), newUser);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError("error register: {}", ex);
                ModelState.AddModelError("", ex.Message);
            }
            ViewBag.Sex = sex;
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }


        public IActionResult RetrievePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RetrievePassword(RetrievePasswordVM emailVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userManagementService.SendPasswordToUser(emailVm.Email);
                    return RedirectToAction(nameof(RetrievePasswordSuccess));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            
            return View(emailVm);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM passwordVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentUserId = Int32.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);
                    var result = _userManagementService.ChangeUserPassword(currentUserId, passwordVm.CurrentPassword, passwordVm.NewPassword);
                    if (result)
                    {
                        HttpContext.SignOutAsync("Cookies");
                        return RedirectToAction("Login", "Account");
                    }
                    ModelState.AddModelError("", "Unable to change password. Try again");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(passwordVm);
        }

        public IActionResult RetrievePasswordSuccess()
        {

            return View();
        }

       

    }
}
