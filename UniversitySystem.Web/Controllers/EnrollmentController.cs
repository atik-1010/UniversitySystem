using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // ==========================
        // ENROLL STUDENT IN COURSE
        // ==========================
        [HttpGet]
        public async Task<IActionResult> Enroll(int courseId)
        {
            var studentId = GetCurrentStudentId();

            if (studentId == 0)
            {
                TempData["Error"] = "Invalid student session.";
                return RedirectToAction("Dashboard", "Student");
            }

            await _enrollmentService.EnrollAsync(studentId, courseId);
            TempData["Success"] = "Successfully enrolled in course.";

            return RedirectToAction("Dashboard", "Student");
        }

        // ==========================
        // UNENROLL STUDENT FROM COURSE
        // ==========================
        [HttpGet]
        public async Task<IActionResult> UnEnroll(int enrollmentId)
        {
            var studentId = GetCurrentStudentId();

            if (studentId == 0)
            {
                TempData["Error"] = "Invalid student session.";
                return RedirectToAction("MyCourses");
            }

            await _enrollmentService.UnEnrollAsync(enrollmentId);
            TempData["Success"] = "Successfully unenrolled from course.";

            return RedirectToAction("MyCourses");
        }

        // ==========================
        // MY COURSES (Enrolled list)
        // ==========================
        [HttpGet]
        public async Task<IActionResult> MyCourses()
        {
            var studentId = GetCurrentStudentId();

            if (studentId == 0)
            {
                TempData["Error"] = "Invalid student session.";
                return RedirectToAction("Dashboard", "Student");
            }

            var enrollments = await _enrollmentService.GetByStudentAsync(studentId);
            return View(enrollments); // strongly typed List<EnrollmentDto>
        }

        // ✅ Helper method to get logged-in student ID
        private int GetCurrentStudentId()
        {
            var claim = User.FindFirst("StudentId");
            return claim != null ? int.Parse(claim.Value) : 0;
        }
    }
}
