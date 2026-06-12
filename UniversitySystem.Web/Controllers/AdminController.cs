using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IStudentService _studentService;

    private readonly IDepartmentService _departmentService;

    private readonly IUserService _userService;

    public AdminController(
        IStudentService studentService,
        IDepartmentService departmentService,
        IUserService userService)
    {
        _studentService = studentService;

        _departmentService = departmentService;

        _userService = userService;
    }

    // =====================================
    // DASHBOARD
    // =====================================
    public async Task<IActionResult> Index()
    {
        var students =
            await _studentService.GetAllAsync();

        var departments =
            await _departmentService.GetAllAsync();

        var users =
            await _userService.GetAllAsync();

        ViewBag.TotalStudents =
            students.Count;

        ViewBag.TotalDepartments =
            departments.Count;

        ViewBag.TotalUsers =
            users.Count;

        ViewBag.TotalTeachers =
            users.Count(x =>
                x.Role == "Teacher");

        ViewBag.TotalAdmins =
            users.Count(x =>
                x.Role == "Admin");

        ViewBag.TotalStudentUsers =
            users.Count(x =>
                x.Role == "Student");

        return View();
    }

    // =====================================
    // USER MANAGEMENT
    // =====================================
    public async Task<IActionResult> Users()
    {
        var users =
            await _userService.GetAllAsync();

        return View(users);
    }

    // =====================================
    // STUDENT MANAGEMENT
    // =====================================
    public async Task<IActionResult> Students()
    {
        var students =
            await _studentService.GetAllAsync();

        return View(students);
    }

    // =====================================
    // REPORT
    // =====================================
    public async Task<IActionResult> Reports()
    {
        var users =
            await _userService.GetAllAsync();

        var students =
            await _studentService.GetAllAsync();

        ViewBag.Users =
            users.Count;

        ViewBag.Students =
            students.Count;

        return View();
    }
}