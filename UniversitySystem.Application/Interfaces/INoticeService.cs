using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces
{
    public interface INoticeService
    {
        Task<List<NoticeDto>> GetAllAsync();
        Task<NoticeDto?> GetByIdAsync(int id);
        Task CreateAsync(NoticeDto dto);
        Task DeleteAsync(int id);
        Task EditAsync(NoticeDto dto);
        Task<List<NoticeDto>> GetLatestAsync(int i);
        Task AddAsync(Notice notice);
        Task UpdateAsync(Notice notice);
    }
}