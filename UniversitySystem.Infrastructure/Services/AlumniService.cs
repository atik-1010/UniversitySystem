using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Infrastructure.Services
{
    public class AlumniService : IAlumniService
    {
        private readonly AppDbContext _context;

        public AlumniService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlumniDto>> GetAllAsync()
        {
            return await _context.Alumni
                .Include(x => x.Department)
                .Select(x => new AlumniDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.Department != null ? x.Department.Name : string.Empty,
                    GraduationYear = x.GraduationYear
                })
                .ToListAsync();
        }

        public async Task<AlumniDto?> GetByIdAsync(int id)
        {
            var alumni = await _context.Alumni
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (alumni == null) return null;

            return new AlumniDto
            {
                Id = alumni.Id,
                Name = alumni.Name,
                Email = alumni.Email,
                DepartmentId = alumni.DepartmentId,
                DepartmentName = alumni.Department?.Name,
                GraduationYear = alumni.GraduationYear
            };
        }

        public async Task CreateAsync(AlumniDto dto)
        {
            var alumni = new Alumni
            {
                Name = dto.Name,
                Email = dto.Email,
                DepartmentId = dto.DepartmentId,
                GraduationYear = dto.GraduationYear
            };

            _context.Alumni.Add(alumni);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(AlumniDto dto)
        {
            var alumni = await _context.Alumni.FindAsync(dto.Id);
            if (alumni != null)
            {
                alumni.Name = dto.Name;
                alumni.Email = dto.Email;
                alumni.DepartmentId = dto.DepartmentId;
                alumni.GraduationYear = dto.GraduationYear;

                _context.Alumni.Update(alumni);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var alumni = await _context.Alumni.FindAsync(id);
            if (alumni != null)
            {
                _context.Alumni.Remove(alumni);
                await _context.SaveChangesAsync();
            }
        }
    }
}
