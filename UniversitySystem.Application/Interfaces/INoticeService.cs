using UniversitySystem.Application.DTOs;

namespace UniversitySystem.Application.Interfaces
{
    public interface INoticeService
    {
        Task<List<NoticeDto>> GetAllAsync();
        Task<NoticeDto?> GetByIdAsync(int id);
        Task CreateAsync(NoticeDto dto);
        Task DeleteAsync(int id);
        Task EditAsync(NoticeDto dto);
    }
}