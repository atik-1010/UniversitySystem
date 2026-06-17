using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Application.DTOs
{
    public class AlumniDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        [Required(ErrorMessage = "Graduation year is required")]
        [Range(1950, 2100, ErrorMessage = "Graduation year must be between 1950 and 2100")]
        public int GraduationYear { get; set; }
    }
}