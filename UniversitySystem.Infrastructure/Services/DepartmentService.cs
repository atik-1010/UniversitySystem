using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Infrastructure.Services;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _context;

    public DepartmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        return await _context.Departments
            .Select(x => new DepartmentDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Department department)
    {
        if (department == null)
            throw new ArgumentNullException(nameof(department));

        var existing = await _context.Departments.FindAsync(department.Id);

        if (existing == null)
            return;

        // নাম আপডেট করা
        existing.Name = department.Name;

        // EF Core কে নিশ্চিত করা যে ডাটা পরিবর্তন হয়েছে
        _context.Entry(existing).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department == null)
            return;

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }
}