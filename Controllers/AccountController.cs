using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Projekt01.Models;

namespace Projekt01.Controllers
{
    public class AccountController : Controller
    {
        private readonly List<AppUser> _users;

        public AccountController()
        {
            _users = new List<AppUser>
            {
                new AppUser { Email = "admin@microsoft.wsei.edu.pl", PasswordHash = HashPassword("admin123"), Role = "Admin" },
                new AppUser { Email = "user@microsoft.wsei.edu.pl", PasswordHash = HashPassword("user123"), Role = "User" }
            };
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            var hashedPassword = HashPassword(password);
            var user = _users.FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index", "Game");
            }

            ViewBag.Error = "Nieprawidłowy email lub hasło.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var email = User.Identity.Name;
            var user = _users.FirstOrDefault(u => u.Email == email);
            
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}
