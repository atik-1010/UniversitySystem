using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Web.Controllers;

public class MarksController : Controller
{
    private readonly IStudentMarkService _markService;
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;

    public MarksController(
        IStudentMarkService markService,
        IStudentService studentService,
        ICourseService courseService)
    {
        _markService = markService;
        _studentService = studentService;
        _courseService = courseService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _markService.GetAllAsync();
        return View(data);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Students = await _studentService.GetAllAsync();
        ViewBag.Courses = await _courseService.GetAllAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudentMark mark)
    {
        await _markService.CreateAsync(mark);
        return RedirectToAction(nameof(Index));
    }
}