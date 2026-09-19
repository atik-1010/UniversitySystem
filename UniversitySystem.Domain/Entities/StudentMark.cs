namespace UniversitySystem.Domain.Entities
{
    public class StudentMark
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public int Score { get; set; }
        public double GradePoint { get; set; }
        public string Grade { get; set; } = "";
    }
}