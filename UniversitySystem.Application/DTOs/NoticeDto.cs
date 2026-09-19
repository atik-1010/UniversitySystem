using System;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Application.DTOs
{
    public class NoticeDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        // ✅ Nullable DateTime with DisplayFormat
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CreatedOn { get; set; }

        // ✅ Optional extra date field if needed
        public DateTime Date { get; set; }

        // ✅ Message should be string, not object
        public string Message { get; set; } = "";
    }
}