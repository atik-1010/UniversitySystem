namespace UniversitySystem.Domain.Entities;

public class Teacher
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Email { get; set; } = "";

    public string Designation { get; set; } = "";

    // FK
    public int DepartmentId { get; set; }

    // Navigation
    public Department? Department { get; set; }
}