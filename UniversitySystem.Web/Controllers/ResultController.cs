using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Web.Controllers
{
    public class ResultController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;

        public ResultController(
            IStudentService studentService,
            ICourseService courseService,
            IEnrollmentService enrollmentService)
        {
            _studentService = studentService;
            _courseService = courseService;
            _enrollmentService = enrollmentService;
        }

        // GET: Result/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: Result/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(int studentId)
        {
            var dto = await BuildTranscript(studentId);
            if (dto == null) return NotFound();

            ViewBag.StudentId = studentId;
            return View("Index", dto);
        }

        // GET: Result/Index (optional direct load)
        public async Task<IActionResult> Index(int studentId)
        {
            var dto = await BuildTranscript(studentId);
            if (dto == null) return NotFound();

            ViewBag.StudentId = studentId;
            return View(dto);
        }

        // GET: Result/Print
        public async Task<IActionResult> Print(int studentId)
        {
            var dto = await BuildTranscript(studentId);
            if (dto == null) return NotFound();

            return View(dto); // Print.cshtml
        }

        // GET: Result/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null) return NotFound();

            var dto = new StudentMarkDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = enrollment.Student?.Name ?? "",
                CourseId = enrollment.CourseId,
                CourseTitle = enrollment.Course?.Title ?? "",
                Score = enrollment.Marks,
                GradePoint = CalculateGradePoint(enrollment.Marks),
                Grade = CalculateGrade(enrollment.Marks)
            };

            return View(dto);
        }

        // POST: Result/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentMarkDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var enrollment = await _enrollmentService.GetByIdAsync(dto.Id);
            if (enrollment == null) return NotFound();

            enrollment.Marks = dto.Score;
            await _enrollmentService.UpdateAsync(enrollment);

            return RedirectToAction("Index", new { studentId = dto.StudentId });
        }

        // GET: Result/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null) return NotFound();

            var dto = new StudentMarkDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = enrollment.Student?.Name ?? "",
                CourseId = enrollment.CourseId,
                CourseTitle = enrollment.Course?.Title ?? "",
                Score = enrollment.Marks,
                GradePoint = CalculateGradePoint(enrollment.Marks),
                Grade = CalculateGrade(enrollment.Marks)
            };

            return View(dto);
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int studentId)
        {
            await _enrollmentService.DeleteAsync(id);
            return RedirectToAction("Index", new { studentId });
        }

        // Helper: Build transcript DTO
        private async Task<StudentResultDto?> BuildTranscript(int studentId)
        {
            var student = await _studentService.GetByIdAsync(studentId);
            if (student == null) return null;

            var enrollments = await _enrollmentService.GetByStudentIdAsync(studentId);

            var results = enrollments.Select(e => new StudentMarkDto
            {
                Id = e.Id,
                StudentId = student.Id,
                StudentName = student.Name,
                CourseId = e.CourseId,
                CourseTitle = e.Course?.Title ?? "",
                Score = e.Marks,
                GradePoint = CalculateGradePoint(e.Marks),
                Grade = CalculateGrade(e.Marks)
            }).ToList();

            return new StudentResultDto
            {
                StudentId = student.Id,
                StudentName = student.Name,
                StudentIdCode = student.StudentCode,
                GPA = results.Any() ? results.Average(r => r.GradePoint) : 0,
                FinalGrade = results.Any() ? CalculateFinalGrade(results.Average(r => r.GradePoint)) : "N/A",
                Results = results
            };
        }

        // Helper methods
        private double CalculateGradePoint(int marks)
        {
            if (marks >= 80) return 4.0;
            if (marks >= 70) return 3.5;
            if (marks >= 60) return 3.0;
            if (marks >= 50) return 2.5;
            if (marks >= 40) return 2.0;
            return 0.0;
        }

        private string CalculateGrade(int marks)
        {
            if (marks >= 80) return "A+";
            if (marks >= 70) return "A";
            if (marks >= 60) return "B";
            if (marks >= 50) return "C";
            if (marks >= 40) return "D";
            return "F";
        }

        private string CalculateFinalGrade(double gpa)
        {
            if (gpa >= 3.75) return "A+";
            if (gpa >= 3.5) return "A";
            if (gpa >= 3.0) return "B";
            if (gpa >= 2.5) return "C";
            if (gpa >= 2.0) return "D";
            return "F";
        }
    }
}
