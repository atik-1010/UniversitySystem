using System.Security.Claims;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllAsync();

    Task<Student?> GetByIdAsync(int id);

    Task AddAsync(Student student);

    Task UpdateAsync(Student student);

    Task DeleteAsync(int id);
    Task<object> GetCurrentStudentIdAsync(ClaimsPrincipal user);
}