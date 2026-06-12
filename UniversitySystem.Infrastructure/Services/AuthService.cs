using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<AppUser> _hasher;

    public AuthService(AppDbContext context)
    {
        _context = context;
        _hasher = new PasswordHasher<AppUser>();
    }

    public async Task<AppUser?> LoginAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null) return null;

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);

        return result == PasswordVerificationResult.Success ? user : null;
    }
}