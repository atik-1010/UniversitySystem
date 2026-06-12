namespace UniversitySystem.Application.DTOs;

public class StudentDto
{
    public int Id { get; set; }

    // নতুন যুক্ত করা হলো
    public string? StudentIdCode { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string DepartmentName { get; set; }
}