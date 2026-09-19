using UniversitySystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniversitySystem.Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task<Enrollment?> GetByIdAsync(int id);
        Task<List<Enrollment>> GetByStudentIdAsync(int studentId);
        Task AddAsync(Enrollment enrollment);
        Task UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(int id);

        // ✅ Correct return type
        Task<List<Course>> GetAvailableCoursesForStudentAsync(int studentId);

        Task EnrollAsync(int studentId, int courseId);
        Task UnEnrollAsync(int enrollmentId);

        Task<List<Enrollment>> GetByStudentAsync(int studentId);
    }
}