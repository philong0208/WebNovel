using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebNovel.Models;
using Microsoft.EntityFrameworkCore;

namespace WebNovel.Controllers.Access
{
    public class AccessController : Controller
    {
        private readonly WebNovelContext _context;

        public AccessController(WebNovelContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if(claimsPrincipal.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var listUser = await _context.Users.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            foreach (User user in listUser)
            {
                if (loginViewModel.userID.Equals(user.UserId)
                    && loginViewModel.password.Equals(user.UserPassword))
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,loginViewModel.userID)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = loginViewModel.keepLoggedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Home");
                }
                //TempData["Error"] = "Sai tên đăng nhập hoặc mật khẩu, hãy thữ lại!";
            }

            ViewData["ValidateMessage"] = "Sai tên đăng nhập hoặc mật khẩu, hãy thữ lại!";
            return View();
        }
    }
}
