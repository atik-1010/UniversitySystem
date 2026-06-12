namespace UniversitySystem.Domain.Entities;

public class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    // Navigation
    public ICollection<Student> Students { get; set; }
        = new List<Student>();

    public ICollection<Course> Courses { get; set; }
        = new List<Course>();

    public ICollection<Teacher> Teachers { get; set; }
        = new List<Teacher>();
}