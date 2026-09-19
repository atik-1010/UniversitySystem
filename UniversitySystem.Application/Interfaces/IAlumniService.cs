using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces
{
    public interface IAlumniService
    {
        Task<List<AlumniDto>> GetAllAsync();
        Task<AlumniDto?> GetByIdAsync(int id);
        Task CreateAsync(AlumniDto dto);
        Task DeleteAsync(int id);
        Task EditAsync(AlumniDto dto);
        Task AddAsync(Alumni alumni);
        Task UpdateAsync(Alumni alumni);
    }
}