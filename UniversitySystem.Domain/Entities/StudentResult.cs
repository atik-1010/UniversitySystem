namespace UniversitySystem.Domain.Entities;

public class StudentResult
{
    public string StudentName { get; set; } = "";

    public double TotalMarks { get; set; }

    public double GPA { get; set; }

    public string Grade { get; set; } = "";
}