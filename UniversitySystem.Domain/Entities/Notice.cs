using System;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Domain.Entities
{
    public class Notice
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        // ✅ Correct type for CreatedOn
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        // ✅ Date field if needed separately
        public DateTime Date { get; set; }

        // ✅ Message should be string, not object
        public string Message { get; set; } = "";
    }
}