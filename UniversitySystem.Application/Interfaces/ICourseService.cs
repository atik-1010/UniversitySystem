using UniversitySystem.Application.DTOs;

namespace UniversitySystem.Application.Interfaces
{
    public interface ICourseService
    {
        // GET ALL
        Task<List<CourseDto>> GetAllAsync();

        // GET BY ID
        Task<CourseDto?> GetByIdAsync(int id);

        // CREATE
        Task CreateAsync(CourseDto dto);

        // UPDATE
        Task UpdateAsync(CourseDto dto);

        // DELETE
        Task DeleteAsync(int id);

        // GET BY DEPARTMENT
        Task<List<CourseDto>> GetByDepartmentAsync(int departmentId);

        // GET BY STUDENT
        Task<List<CourseDto>> GetByStudentIdAsync(object studentId);
    }
}