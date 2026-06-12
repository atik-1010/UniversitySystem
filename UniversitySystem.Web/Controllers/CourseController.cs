using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Web.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly AppDbContext _context;

    public CourseController(ICourseService courseService, AppDbContext context)
    {
        _courseService = courseService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _courseService.GetAllAsync();
        return View(data);
    }

    public IActionResult Create()
    {
        ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        await _courseService.CreateAsync(course);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null) return NotFound();

        ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Course course)
    {
        await _courseService.UpdateAsync(course);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _courseService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}