using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces;

public interface ITeacherService
{
    Task<List<Teacher>> GetAllAsync();
    Task<Teacher?> GetByIdAsync(int id);
    Task CreateAsync(Teacher teacher);
    Task UpdateAsync(Teacher teacher);
    Task DeleteAsync(int id);
}