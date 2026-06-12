using AutoMapper;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Department
        CreateMap<Department, DepartmentDto>().ReverseMap();

        // Student
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department.Name));
    }
}