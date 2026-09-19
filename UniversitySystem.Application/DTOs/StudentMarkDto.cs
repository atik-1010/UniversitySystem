using Microsoft.AspNetCore.Mvc.Razor;

namespace UniversitySystem.Application.DTOs
{
    public class StudentMarkDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = "";
        public int Score { get; set; }
        public double GradePoint { get; set; }
        public string Grade { get; set; } = "";
        public Func<object, HelperResult> Course { get; set; }

        public HelperResult Marks(object arg)
        {
            throw new NotImplementedException();
        }
    }
}