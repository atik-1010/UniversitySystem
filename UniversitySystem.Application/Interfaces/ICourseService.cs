using UniversitySystem.Application.DTOs;

namespace UniversitySystem.Application.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllAsync();
        Task<CourseDto?> GetByIdAsync(int id);
        Task CreateAsync(CourseDto dto);
        Task UpdateAsync(CourseDto dto);
        Task DeleteAsync(int id);
    }
}