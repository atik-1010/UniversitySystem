using System;

namespace UniversitySystem.Application.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public string? CourseTitle { get; set; }
        public DateTime EnrolledOn { get; set; }
        public object StudentName { get; set; }
        public object DepartmentName { get; set; }
    }
}