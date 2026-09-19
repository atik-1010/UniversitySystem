using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;
using static UniversitySystem.Infrastructure.Class1;

namespace UniversitySystem.Application.Services
{
    public class StudentMarkService : IStudentMarkService
    {
        private readonly AppDbContext _context;

        public StudentMarkService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentMark>> GetAllAsync()
        {
            return await _context.StudentMarks
                .Include(m => m.Student)
                .Include(m => m.Course)
                .ToListAsync();
        }

        public async Task CreateAsync(StudentMark mark)
        {
            _context.StudentMarks.Add(mark);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentResultDto?> GetStudentResultAsync(int studentId)
        {
            var marks = await _context.StudentMarks
                .Include(m => m.Student)
                .Include(m => m.Course)
                .Where(m => m.StudentId == studentId)
                .ToListAsync();

            if (!marks.Any()) return null;

            double gpa = marks.Average(m => m.GradePoint);

            return new StudentResultDto
            {
                StudentId = studentId,
                StudentName = marks.First().Student?.Name ?? "",
                GPA = gpa,
                Marks = marks.Select(m => new StudentMarkDto
                {
                    Id = m.Id,
                    StudentId = m.StudentId,
                    StudentName = m.Student?.Name ?? "",
                    CourseId = m.CourseId,
                    CourseTitle = m.Course?.Title ?? "",
                    Score = m.Score,
                    GradePoint = m.GradePoint,
                    Grade = m.Grade
                }).ToList()
            };
        }

        public async Task<List<StudentMarkDto>> GetByStudentIdAsync(int studentId)
        {
            var marks = await _context.StudentMarks
                .Include(m => m.Student)
                .Include(m => m.Course)
                .Where(m => m.StudentId == studentId)
                .ToListAsync();

            return marks.Select(m => new StudentMarkDto
            {
                Id = m.Id,
                StudentId = m.StudentId,
                StudentName = m.Student?.Name ?? "",
                CourseId = m.CourseId,
                CourseTitle = m.Course?.Title ?? "",
                Score = m.Score,
                GradePoint = m.GradePoint,
                Grade = m.Grade
            }).ToList();
        }
    }
}
