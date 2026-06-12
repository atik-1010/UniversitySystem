using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL (DTO)
    // =========================
    public async Task<List<StudentDto>> GetAllAsync()
    {
        return await _context.Students
            .Include(x => x.Department)
            .Select(x => new StudentDto
            {
                Id = x.Id,
                StudentIdCode = x.StudentIdCode ?? "N/A",
                Name = x.Name ?? "N/A",
                Email = x.Email ?? "N/A",
                DepartmentName = x.Department != null ? x.Department.Name : "N/A"
            })
            .ToListAsync();
    }

    // =========================
    // GET BY ID (NULL SAFE)
    // =========================
    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    // =========================
    // ADD (SAFE INSERT)
    // =========================
    public async Task AddAsync(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));

        student.Id = 0;

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    // =========================
    // UPDATE (SAFE UPDATE)
    // =========================
    public async Task UpdateAsync(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));

        var existing = await _context.Students
            .FirstOrDefaultAsync(x => x.Id == student.Id);

        if (existing == null)
            return;

        existing.StudentIdCode = student.StudentIdCode;
        existing.Name = student.Name;
        existing.Email = student.Email;
        existing.DepartmentId = student.DepartmentId;

        await _context.SaveChangesAsync();
    }

    // =========================
    // DELETE (SAFE DELETE)
    // =========================
    public async Task DeleteAsync(int id)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student == null)
            return;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}