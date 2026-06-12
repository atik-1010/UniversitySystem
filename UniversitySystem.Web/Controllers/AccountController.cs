using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversitySystem.Infrastructure.Data;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Web.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    // ================= LOGIN =================
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string username, string password)
    {
        // 🔥 NULL SAFE CHECK
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "Username and Password are required";
            return View();
        }

        var user = _context.Users.FirstOrDefault(x => x.Username == username);

        if (user == null)
        {
            ViewBag.Error = "Invalid login";
            return View();
        }

        // 🔐 Password verification
        var hasher = new PasswordHasher<AppUser>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed)
        {
            ViewBag.Error = "Invalid login";
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName ?? ""),
            new Claim(ClaimTypes.Role, user.Role ?? "Student"),
            new Claim("UserId", user.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        // ================= ROLE BASED REDIRECT =================
        switch (user.Role)
        {
            case "Admin":
                return RedirectToAction("Index", "Admin");

            case "Teacher":
                return RedirectToAction("Index", "Teacher");

            default:
                return RedirectToAction("Index", "Student");
        }
    }

    // ================= LOGOUT =================
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    // ================= REGISTER =================
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(AppUser user, string password)
    {
        // 🔥 NULL CHECK
        if (string.IsNullOrWhiteSpace(user.Username) ||
            string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "All fields are required";
            return View();
        }

        // duplicate username check
        if (_context.Users.Any(x => x.Username == user.Username))
        {
            ViewBag.Error = "Username already exists";
            return View();
        }

        // 🔥 CREATE USER (ROLE FROM VIEW DROPDOWN)
        var newUser = new AppUser
        {
            Username = user.Username,
            FullName = user.FullName,
            Role = string.IsNullOrWhiteSpace(user.Role) ? "Student" : user.Role
        };

        // password hashing
        var hasher = new PasswordHasher<AppUser>();
        newUser.PasswordHash = hasher.HashPassword(newUser, password);

        _context.Users.Add(newUser);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }
}