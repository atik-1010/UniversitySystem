using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Domain.Entities
{
    public class Alumni
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        [Required]
        [Range(1950, 2100)]
        public int GraduationYear { get; set; }
    }
}