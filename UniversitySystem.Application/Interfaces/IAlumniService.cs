using UniversitySystem.Application.DTOs;

namespace UniversitySystem.Application.Interfaces
{
    public interface IAlumniService
    {
        Task<List<AlumniDto>> GetAllAsync();
        Task<AlumniDto?> GetByIdAsync(int id);
        Task CreateAsync(AlumniDto dto);
        Task DeleteAsync(int id);
        Task EditAsync(AlumniDto dto);
    }
}