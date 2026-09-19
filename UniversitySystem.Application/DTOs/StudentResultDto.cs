namespace UniversitySystem.Application.DTOs
{
    public class StudentResultDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public string StudentIdCode { get; set; } = "";

        // GPA and Final Grade
        public double GPA { get; set; }
        public string FinalGrade { get; set; } = "";

        // List of marks per course
        public List<StudentMarkDto> Results { get; set; } = new();
        public List<StudentMarkDto> Marks { get; set; }
    }
}