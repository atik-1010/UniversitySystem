using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces;

public interface ICourseService
{
    Task<List<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task CreateAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(int id);
}