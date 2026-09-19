using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentDashboardController : Controller
    {
        private readonly INoticeService _noticeService;
        private readonly ICourseService _courseService;
        private readonly IStudentMarkService _markService;
        private readonly IStudentService _studentService;

        public StudentDashboardController(
            INoticeService noticeService,
            ICourseService courseService,
            IStudentMarkService markService,
            IStudentService studentService)
        {
            _noticeService = noticeService;
            _courseService = courseService;
            _markService = markService;
            _studentService = studentService;
        }

        // ==========================
        // INDEX (Dashboard)
        // ==========================
        public async Task<IActionResult> Index()
        {
            int studentId = (int)await _studentService.GetCurrentStudentIdAsync(User);

            var dashboard = new StudentDashboardDto
            {
                Notices = await _noticeService.GetLatestAsync(5),
                Courses = await _courseService.GetByStudentIdAsync(studentId),
                Marks = await _markService.GetByStudentIdAsync(studentId)
            };

            return View(dashboard);
        }
    }
}