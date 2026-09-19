namespace UniversitySystem.Application.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Code { get; set; } = "";
        public int Credit { get; set; }
        public string DepartmentName { get; set; } = "";
        public int DepartmentId { get; set; }
    }
}