using FluentValidation;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Validators;

public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.DepartmentId)
            .NotEmpty();
    }
}