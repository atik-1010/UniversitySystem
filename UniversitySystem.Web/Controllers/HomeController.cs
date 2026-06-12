using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Infrastructure.Data;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.StudentCount = _context.Students.Count();
        ViewBag.DepartmentCount = _context.Departments.Count();
        ViewBag.UserCount = _context.Users.Count();

        return View();
    }
}