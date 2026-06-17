using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<CourseDto>> GetAllAsync()
        {
            return await _context.Courses
                .Include(x => x.Department)
                .Select(x => new CourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Code = x.Code,
                    Credit = x.Credit,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.Department != null ? x.Department.Name : string.Empty
                })
                .ToListAsync();
        }

        // GET BY ID
        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var course = await _context.Courses
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Code = course.Code,
                Credit = course.Credit,
                DepartmentId = course.DepartmentId,
                DepartmentName = course.Department?.Name
            };
        }

        // CREATE
        public async Task CreateAsync(CourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title ?? string.Empty,
                Code = dto.Code ?? string.Empty,
                Credit = dto.Credit,
                DepartmentId = dto.DepartmentId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        // UPDATE
        public async Task UpdateAsync(CourseDto dto)
        {
            var course = await _context.Courses.FindAsync(dto.Id);
            if (course == null) return;

            course.Title = dto.Title ?? string.Empty;
            course.Code = dto.Code ?? string.Empty;
            course.Credit = dto.Credit;
            course.DepartmentId = dto.DepartmentId;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
