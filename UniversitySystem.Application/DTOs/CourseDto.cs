namespace UniversitySystem.Application.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Code { get; set; }

        public double Credit { get; set; }

        // Foreign Key
        public int DepartmentId { get; set; }

        // Department name for display
        public string? DepartmentName { get; set; }
        public object Department { get; set; }
    }
}