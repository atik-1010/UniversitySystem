using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Domain.Entities
{
    public class Notice
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = "";

        [Required]
        public DateTime Date { get; set; }
    }
}