using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces;

public interface IAuthService
{
    Task<AppUser?> LoginAsync(string username, string password);
}