using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Domain.Entities;

public class Student
{
    public int Id { get; set; }

    [Required]
    public string StudentIdCode { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";

    // FK
    public int DepartmentId { get; set; }

    // Navigation
    public Department? Department { get; set; }

    // 🔥 READY FOR STEP 8 (Marks system)
    public ICollection<StudentMark> StudentMarks { get; set; }
        = new List<StudentMark>();
}