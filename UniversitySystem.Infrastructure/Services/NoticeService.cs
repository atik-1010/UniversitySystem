using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Infrastructure.Data;

namespace UniversitySystem.Infrastructure.Services
{
    public class NoticeService : INoticeService
    {
        private readonly AppDbContext _context;

        public NoticeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<NoticeDto>> GetAllAsync()
        {
            return await _context.Notices
                .Select(x => new NoticeDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Date = x.Date
                })
                .ToListAsync();
        }

        public async Task<NoticeDto?> GetByIdAsync(int id)
        {
            var notice = await _context.Notices.FindAsync(id);
            if (notice == null) return null;

            return new NoticeDto
            {
                Id = notice.Id,
                Title = notice.Title,
                Description = notice.Description,
                Date = notice.Date
            };
        }

        public async Task CreateAsync(NoticeDto dto)
        {
            var notice = new Notice
            {
                Title = dto.Title,
                Description = dto.Description,
                Date = dto.Date
            };

            _context.Notices.Add(notice);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(NoticeDto dto)
        {
            var notice = await _context.Notices.FindAsync(dto.Id);
            if (notice != null)
            {
                notice.Title = dto.Title;
                notice.Description = dto.Description;
                notice.Date = dto.Date;

                _context.Notices.Update(notice);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<NoticeDto>> GetLatestAsync(int i)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Notice notice)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Notice notice)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var notice = await _context.Notices.FindAsync(id);
            if (notice != null)
            {
                _context.Notices.Remove(notice);
                await _context.SaveChangesAsync();
            }
        }
    }
}
