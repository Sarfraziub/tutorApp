using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Claims;
using TutorApp.Website.Context;
using TutorApp.Website.Models;
using TutorApp.Website.ViewModels;

namespace TutorApp.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly PasswordHasher<Student> _passwordHasher;
        private readonly PasswordHasher<Admin> _passwordAdminHasher;

        public AccountController(SchoolDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Student>();
            _passwordAdminHasher = new PasswordHasher<Admin>();
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassId = model.ClassId,
                    IsApproved = true 
                };

                student.PasswordHash = _passwordHasher.HashPassword(student, model.Password);
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the student by email
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Email == model.Email);

                // If student is found, verify the password
                if (student != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(student, student.PasswordHash, model.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        // Create authentication claims
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, student.Email),
                            new Claim(ClaimTypes.NameIdentifier, student.StudentId.ToString()),
                            new Claim("StudentName", student.Name.ToString()),
                            new Claim("ClassId", student.ClassId.ToString()),
							new Claim(ClaimTypes.Role, Roles.Student)
                            // You can add more claims if needed
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            // Optional settings, e.g. whether to persist the cookie across sessions
                            //IsPersistent = model.RememberMe
                        };

                        // Sign in the user
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                      new ClaimsPrincipal(claimsIdentity), authProperties);

                        // Redirect to the homepage or dashboard after successful login
                        return RedirectToAction("Dashboard", "Student");
                    }
                }

                // If student is not found or password is incorrect
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        // POST: Login
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the homepage or login page after logout
            return RedirectToAction("Index", "Home");
        }
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
