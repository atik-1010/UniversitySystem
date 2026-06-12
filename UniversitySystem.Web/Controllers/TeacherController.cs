using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Application.DTOs;

namespace UniversitySystem.Web.Controllers;

[Authorize(Roles = "Teacher")]
public class TeacherController : Controller
{
    private readonly IStudentService _studentService;
    private readonly IDepartmentService _departmentService;
    private readonly ICourseService _courseService;
    private readonly ITeacherService _teacherService;

    public TeacherController(
        IStudentService studentService,
        IDepartmentService departmentService,
        ICourseService courseService,
        ITeacherService teacherService)
    {
        _studentService = studentService;
        _departmentService = departmentService;
        _courseService = courseService;
        _teacherService = teacherService;
    }

    // LIST
    public async Task<IActionResult> Index()
    {
        var teachers = await _teacherService.GetAllAsync();
        return View(teachers);
    }

    // CREATE GET
    public async Task<IActionResult> Create()
    {
        var departments = await _departmentService.GetAllAsync();

        ViewBag.Departments = departments ?? new List<DepartmentDto>();

        return View();
    }

    // CREATE POST
    [HttpPost]
    public async Task<IActionResult> Create(Teacher teacher)
    {
        if (!ModelState.IsValid || teacher.DepartmentId == 0)
        {
            ModelState.AddModelError("", "Please fill all required fields.");

            ViewBag.Departments = await _departmentService.GetAllAsync()
                ?? new List<DepartmentDto>();

            return View(teacher);
        }

        await _teacherService.CreateAsync(teacher);
        return RedirectToAction(nameof(Index));
    }

    // EDIT GET
    public async Task<IActionResult> Edit(int id)
    {
        var teacher = await _teacherService.GetByIdAsync(id);
        if (teacher == null) return NotFound();

        ViewBag.Departments = await _departmentService.GetAllAsync()
            ?? new List<DepartmentDto>();

        return View(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Teacher teacher)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = await _departmentService.GetAllAsync()
                ?? new List<DepartmentDto>();

            return View(teacher);
        }

        await _teacherService.UpdateAsync(teacher);
        return RedirectToAction(nameof(Index));
    }

    // DELETE
    public async Task<IActionResult> Delete(int id)
    {
        await _teacherService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}