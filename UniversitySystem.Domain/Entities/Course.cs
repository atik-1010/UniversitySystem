using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Code { get; set; } = "";

        [Required]
        public int Credit { get; set; }   // ✅ int type

        // Foreign Key
        public int DepartmentId { get; set; }

        // Navigation property
        public Department? Department { get; set; }
        
        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}