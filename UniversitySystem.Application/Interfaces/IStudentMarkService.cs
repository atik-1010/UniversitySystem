using System.Collections.Generic;
using System.Threading.Tasks;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Application.DTOs;

namespace UniversitySystem.Application.Interfaces
{
    public interface IStudentMarkService
    {
        Task<List<StudentMark>> GetAllAsync();
        Task CreateAsync(StudentMark mark);
        Task<StudentResultDto?> GetStudentResultAsync(int studentId);
        Task<List<StudentMarkDto>> GetByStudentIdAsync(int studentId);
    }
}