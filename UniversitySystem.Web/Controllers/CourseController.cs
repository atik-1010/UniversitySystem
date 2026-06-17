using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs; // use DTOs
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly AppDbContext _context;

        public CourseController(ICourseService courseService, AppDbContext context)
        {
            _courseService = courseService;
            _context = context;
        }

        // ==========================
        // INDEX
        // ==========================
        public async Task<IActionResult> Index()
        {
            var data = await _courseService.GetAllAsync(); // returns List<CourseDto>
            return View(data);
        }

        // ==========================
        // CREATE GET
        // ==========================
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // ==========================
        // CREATE POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseDto course)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
                return View(course);
            }

            await _courseService.CreateAsync(course);
            TempData["Success"] = "Course Added";
            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // EDIT GET
        // ==========================
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null) return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // ==========================
        // EDIT POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseDto course)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
                return View(course);
            }

            await _courseService.UpdateAsync(course);
            TempData["Success"] = "Course Updated";
            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // DELETE
        // ==========================
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);
            TempData["Success"] = "Course Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
