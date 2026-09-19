using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UniversitySystem.Web.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IEnrollmentService _enrollmentService;

        public StudentController(
            IStudentService studentService,
            IDepartmentService departmentService,
            IEnrollmentService enrollmentService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
            _enrollmentService = enrollmentService;
        }

        // ==========================
        // STUDENT DASHBOARD
        // ==========================
        public async Task<IActionResult> Dashboard()
        {
            var studentId = GetCurrentStudentId();
            var courses = await _enrollmentService.GetAvailableCoursesForStudentAsync(studentId);

            var courseDtos = new List<CourseDto>();
            foreach (var c in courses)
            {
                courseDtos.Add(new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Code = c.Code,
                    Credit = c.Credit,
                    DepartmentId = c.DepartmentId,
                    DepartmentName = c.Department != null ? c.Department.Name : string.Empty
                });
            }

            return View(courseDtos);
        }

        // ==========================
        // INDEX
        // ==========================
        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            const int pageSize = 8;
            var students = await _studentService.GetAllAsync();
            var studentList = students.ToList();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                var filtered = new List<StudentDto>();
                foreach (var x in studentList)
                {
                    if ((x.StudentIdCode ?? "").ToLower().Contains(search) ||
                        (x.Name ?? "").ToLower().Contains(search) ||
                        (x.Email ?? "").ToLower().Contains(search) ||
                        (x.DepartmentName ?? "").ToLower().Contains(search))
                    {
                        filtered.Add(x);
                    }
                }
                studentList = filtered;
            }

            ViewBag.TotalStudents = studentList.Count;
            ViewBag.TotalDepartments = studentList
                .Select(x => x.DepartmentName)
                .Distinct()
                .Count();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(studentList.Count / (double)pageSize);
            ViewBag.Search = search;

            var pagedStudents = studentList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(pagedStudents);
        }

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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            TempData["Success"] = "Student Deleted";
            return RedirectToAction(nameof(Index));
        }

        private int GetCurrentStudentId()
        {
            var claim = User.FindFirst("StudentId");
            return claim != null ? int.Parse(claim.Value) : 0;
        }
    }
}
