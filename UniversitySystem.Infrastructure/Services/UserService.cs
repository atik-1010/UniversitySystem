using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AppUser>> GetAllAsync()
        => await _context.Users.ToListAsync();

    public async Task<AppUser?> GetByIdAsync(int id)
        => await _context.Users.FindAsync(id);

    public async Task CreateAsync(AppUser user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AppUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}