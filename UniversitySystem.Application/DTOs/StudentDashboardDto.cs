using System.Collections.Generic;

namespace UniversitySystem.Application.DTOs
{
    public class StudentDashboardDto
    {
        public List<NoticeDto> Notices { get; set; } = new List<NoticeDto>();
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
        public List<StudentMarkDto> Marks { get; set; } = new List<StudentMarkDto>();
    }
}