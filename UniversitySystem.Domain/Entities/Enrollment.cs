using System;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        // ✅ Marks property
        public int Marks { get; set; }

        // ✅ EnrolledOn must be DateTime, not object
        public DateTime EnrolledOn { get; set; } = DateTime.UtcNow;
    }
}