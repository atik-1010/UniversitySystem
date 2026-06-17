using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Web.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;

        public StudentController(
            IStudentService studentService,
            IDepartmentService departmentService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
        }

        // ==========================
        // STUDENT DASHBOARD
        // ==========================
        public async Task<IActionResult> Dashboard()
        {
            var students = await _studentService.GetAllAsync(); // returns List<StudentDto>

            ViewBag.TotalStudents = students.Count;
            ViewBag.TotalDepartments = students
                .Select(x => x.DepartmentName)
                .Distinct()
                .Count();

            return View(students); // pass typed List<StudentDto>
        }

        // ==========================
        // INDEX
        // ==========================
        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            const int pageSize = 8;
            var students = await _studentService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                students = students
                    .Where(x =>
                        (x.StudentIdCode ?? "").ToLower().Contains(search) ||
                        (x.Name ?? "").ToLower().Contains(search) ||
                        (x.Email ?? "").ToLower().Contains(search) ||
                        (x.DepartmentName ?? "").ToLower().Contains(search)
                    )
                    .ToList();
            }

            ViewBag.TotalStudents = students.Count;
            ViewBag.TotalDepartments = students
                .Select(x => x.DepartmentName)
                .Distinct()
                .Count();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(students.Count / (double)pageSize);
            ViewBag.Search = search;

            students = students
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(students); // typed List<StudentDto>
        }

        // ==========================
        // CREATE GET
        // ==========================
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(
                await _departmentService.GetAllAsync(),
                "Id",
                "Name"
            );
            return View();
        }

        // ==========================
        // CREATE POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(
                    await _departmentService.GetAllAsync(),
                    "Id",
                    "Name",
                    student.DepartmentId
                );
                return View(student);
            }

            await _studentService.AddAsync(student);
            TempData["Success"] = "Student Added";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // DELETE
        // ==========================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            TempData["Success"] = "Student Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
