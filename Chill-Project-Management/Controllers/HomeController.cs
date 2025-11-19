using Chill_Project_Management.Models;
using Chill_Project_Management.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BCrypt.Net;

namespace Chill_Project_Management.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

           
            

        public IActionResult Login()
        {
            return View();
    
        }


        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check duplicate username/email
            var exists = await _context.Users
                .AnyAsync(u => u.Username == model.Username || u.Email == model.Email);

            if (exists)
            {
                ModelState.AddModelError("", "Username or email already taken");
                return View(model);
            }

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Username = model.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);               // ← your existing DbContext
            await _context.SaveChangesAsync();      // ← this creates the user

            // Optional: show success message
            TempData["Success"] = "Account created successfully! Please log in.";

            return RedirectToAction("Login");
        }
        {
            if (!ModelState.IsValid)
                return View(model); // shows validation errors automatically

            // Manually map ONLY safe fields → now 100% safe from over-posting
            var user = new User
            {
                Email = model.Email,
                Username = model.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                // IsAdmin = false → NEVER comes from user input
                // Role = "User" → hardcoded or from config
            };

            await _userManager.CreateAsync(user);
            return RedirectToAction("Login");
        }

        public IActionResult Verify()
        {
            return View();
        }
        public IActionResult CreateOrJoin()
        {
            return View();
        }
        public IActionResult Workitems()
        {
            return View();
        }
          
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult TestAgain()
        {
            return View();
        }
          



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
