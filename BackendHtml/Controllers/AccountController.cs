using Microsoft.AspNetCore.Mvc;
using BackendHtml.Context;
using LoginRegisterExample.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BackendHtml.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            // Kiểm tra tên đăng nhập và mật khẩu
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                // Tạo claims
                List<Claim> claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, user.Fullname),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                // List<Claim> claims = new List<Claim>{
                //     new Claim(ClaimTypes.Name, member.Fullname),
                //     new Claim(ClaimTypes.Email, member.Email),
                //     new Claim(ClaimTypes.NameIdentifier, member.MemberId.ToString())
                // };

                ;

                // Tạo identity và principal
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                // Đăng nhập và lưu cookie
                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = true
                });


                TempData["Message"] = "Đăng nhập thành công!";
                return Redirect("/AI/ADD"); // Chuyển hướng đến trang AI
            }
            else
            {
                ViewBag.Message = "Đăng nhập thất bại. Sai tên hoặc mật khẩu.";
                return View();
            }
        }

        // Trang đăng ký
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Xử lý đăng ký
        [HttpPost]
        public IActionResult Register(string username, string fullname, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Message = "Tên đăng nhập đã tồn tại!";
                return View();
            }

            var newUser = new User
            {
                Username = username,
                Fullname = fullname,
                PasswordHash = PasswordHasher.HashPassword(password)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            ViewBag.Message = "Đăng ký thành công!";
            return View("Success");
        }
    }
}
