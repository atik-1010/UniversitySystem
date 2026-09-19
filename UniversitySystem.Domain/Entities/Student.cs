using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string StudentIdCode { get; set; } = "";

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        // Foreign Key
        public int DepartmentId { get; set; }

        // Navigation property
        public Department? Department { get; set; }

        // Marks system
        public ICollection<StudentMark> StudentMarks { get; set; } = new List<StudentMark>();
        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public string StudentCode { get; set; }
    }
}