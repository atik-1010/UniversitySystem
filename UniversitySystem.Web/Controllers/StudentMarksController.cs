using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Web.Controllers
{
    [Authorize]
    public class StudentMarksController : Controller
    {
        private readonly IStudentMarkService _markService;

        public StudentMarksController(IStudentMarkService markService)
        {
            _markService = markService;
        }

        // ==========================
        // INDEX (All Marks)
        // ==========================
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Index()
        {
            var marks = await _markService.GetAllAsync();
            return View(marks);
        }

        // ==========================
        // CREATE (Add Mark)
        // ==========================
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(StudentMark mark)
        {
            if (ModelState.IsValid)
            {
                await _markService.CreateAsync(mark);
                return RedirectToAction(nameof(Index));
            }
            return View(mark);
        }

        // ==========================
        // RESULT (Transcript for Student)
        // ==========================
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Result(int studentId)
        {
            var result = await _markService.GetStudentResultAsync(studentId);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}