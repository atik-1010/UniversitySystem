using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Domain.Entities
{
    public class Alumni
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        [Required]
        public int GraduationYear { get; set; }

        // Foreign key
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        // ✅ If you want DepartmentName only for view, mark it NotMapped
        [NotMapped]
        public string DepartmentName => Department?.Name ?? string.Empty;
    }
}