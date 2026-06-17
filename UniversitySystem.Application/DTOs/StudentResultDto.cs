namespace UniversitySystem.Application.DTOs;

public class StudentResultDto
{
    public string StudentName { get; set; } = "";

    public string StudentIdCode { get; set; } = "";

    public List<ResultRowDto> Results { get; set; }
        = new();

    public double GPA { get; set; }

    public string FinalGrade { get; set; } = "";
}

public class ResultRowDto
{
    public string Course { get; set; } = "";

    public int Marks { get; set; }

    public double GradePoint { get; set; }

    public string Grade { get; set; } = "";
}