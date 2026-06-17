using System.Collections.Generic;

namespace UniversitySystem.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        // Navigation properties
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}