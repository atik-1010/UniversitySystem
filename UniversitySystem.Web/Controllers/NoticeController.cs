using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs;

namespace UniversitySystem.Web.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student")]
    public class NoticeController : Controller
    {
        private readonly INoticeService _noticeService;

        public NoticeController(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        public async Task<IActionResult> Index()
        {
            var notices = await _noticeService.GetAllAsync();
            return View(notices);
        }

        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(NoticeDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _noticeService.CreateAsync(dto);
            TempData["Success"] = "Notice Added";
            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // EDIT GET
        // ==========================
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int id)
        {
            var notice = await _noticeService.GetByIdAsync(id);
            if (notice == null) return NotFound();
            return View(notice);
        }

        // ==========================
        // EDIT POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(NoticeDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _noticeService.EditAsync(dto);
            TempData["Success"] = "Notice Updated";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var notice = await _noticeService.GetByIdAsync(id);
            if (notice == null) return NotFound();
            return View(notice);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _noticeService.DeleteAsync(id);
            TempData["Success"] = "Notice Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
