using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces;

public interface IUserService
{
    Task<List<AppUser>> GetAllAsync();
    Task<AppUser?> GetByIdAsync(int id);
    Task CreateAsync(AppUser user);
    Task UpdateAsync(AppUser user);
    Task DeleteAsync(int id);
}