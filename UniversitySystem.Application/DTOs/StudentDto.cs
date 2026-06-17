namespace UniversitySystem.Application.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string StudentIdCode { get; set; } = "";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}