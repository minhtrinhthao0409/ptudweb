using Microsoft.AspNetCore.Mvc;
using BachendHtml.Context;
using LoginRegisterExample.Models;

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
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                ViewBag.Message = "Đăng nhập thành công!";
                return View("Success");
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
