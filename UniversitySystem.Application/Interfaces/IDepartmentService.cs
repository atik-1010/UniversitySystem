using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces;

public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllAsync();

    Task<Department?> GetByIdAsync(int id);

    Task AddAsync(Department department);

    Task UpdateAsync(Department department);

    Task DeleteAsync(int id);
}