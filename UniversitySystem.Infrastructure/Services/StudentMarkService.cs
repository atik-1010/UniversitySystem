using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Infrastructure.Services;

public class StudentMarkService : IStudentMarkService
{
    private readonly AppDbContext _context;

    public StudentMarkService(
        AppDbContext context)
    {
        _context = context;
    }

    // ======================
    // ALL MARKS
    // ======================
    public async Task<List<StudentMark>>
        GetAllAsync()
    {
        return await _context
            .StudentMarks
            .Include(x => x.Student)
            .Include(x => x.Course)
            .ToListAsync();
    }

    // ======================
    // CREATE
    // ======================
    public async Task CreateAsync(
        StudentMark mark)
    {
        _context
            .StudentMarks
            .Add(mark);

        await _context
            .SaveChangesAsync();
    }

    // ======================
    // RESULT
    // ======================
    public async Task<StudentResultDto?>
        GetStudentResultAsync(
            int studentId)
    {
        var marks =
            await _context
            .StudentMarks
            .Include(x => x.Student)
            .Include(x => x.Course)
            .Where(
                x =>
                    x.StudentId
                    ==
                    studentId
            )
            .ToListAsync();

        if (!marks.Any())
            return null;

        var result =
            new StudentResultDto();

        result.StudentName =
            marks
            .First()
            .Student?
            .Name
            ??
            "";

        result.StudentIdCode =
            marks
            .First()
            .Student?
            .StudentIdCode
            ??
            "";

        foreach (
            var m
            in marks
        )
        {
            var gp =
                CalculateGP(
                    m.Marks
                );

            result
                .Results
                .Add(
                    new ResultRowDto
                    {
                        Course =
                            m.Course?.Title
                            ??
                            "-",

                        Marks =
                            m.Marks,

                        GradePoint =
                            gp,

                        Grade =
                            CalculateGrade(
                                m.Marks
                            )
                    }
                );
        }

        result.GPA =
            Math.Round(
                result.Results
                .Average(
                    x =>
                        x.GradePoint
                ),
                2
            );

        result.FinalGrade =
            CalculateFinalGrade(
                result.GPA
            );

        return result;
    }

    // ======================
    // GPA
    // ======================

    private double CalculateGP(
        int marks)
    {
        if (marks >= 80)
            return 4.00;

        if (marks >= 75)
            return 3.75;

        if (marks >= 70)
            return 3.50;

        if (marks >= 65)
            return 3.25;

        if (marks >= 60)
            return 3.00;

        if (marks >= 55)
            return 2.75;

        if (marks >= 50)
            return 2.50;

        if (marks >= 45)
            return 2.25;

        if (marks >= 40)
            return 2.00;

        return 0;
    }

    // ======================
    // LETTER
    // ======================

    private string CalculateGrade(
        int marks)
    {
        if (marks >= 80)
            return "A+";

        if (marks >= 75)
            return "A";

        if (marks >= 70)
            return "A-";

        if (marks >= 65)
            return "B+";

        if (marks >= 60)
            return "B";

        if (marks >= 55)
            return "B-";

        if (marks >= 50)
            return "C+";

        if (marks >= 45)
            return "C";

        if (marks >= 40)
            return "D";

        return "F";
    }

    // ======================
    // FINAL
    // ======================

    private string CalculateFinalGrade(
        double gpa)
    {
        if (gpa >= 4)
            return "A+";

        if (gpa >= 3.75)
            return "A";

        if (gpa >= 3.50)
            return "A-";

        if (gpa >= 3)
            return "B";

        if (gpa >= 2)
            return "C";

        return "F";
    }
}