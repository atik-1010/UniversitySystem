namespace UniversitySystem.Domain.Entities;

public class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Code { get; set; } = "";

    public int Credit { get; set; }

    // FK
    public int DepartmentId { get; set; }

    // Navigation
    public Department? Department { get; set; }
}