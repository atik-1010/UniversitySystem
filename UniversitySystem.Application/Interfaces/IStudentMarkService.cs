using UniversitySystem.Domain.Entities;
using UniversitySystem.Application.DTOs;

namespace UniversitySystem.Application.Interfaces;

public interface IStudentMarkService
{
    Task<List<StudentMark>> GetAllAsync();

    Task CreateAsync(StudentMark mark);

    // STEP 9
    Task<StudentResultDto?> GetStudentResultAsync(int studentId);
}