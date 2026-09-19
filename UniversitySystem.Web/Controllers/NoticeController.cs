using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;

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

        // ==========================
        // INDEX (All Notices)
        // ==========================
        public async Task<IActionResult> Index()
        {
            var notices = await _noticeService.GetAllAsync();
            return View(notices); // typed List<NoticeDto>
        }

        // ==========================
        // DETAILS
        // ==========================
        public async Task<IActionResult> Details(int id)
        {
            var notice = await _noticeService.GetByIdAsync(id);
            if (notice == null)
            {
                TempData["Error"] = "Notice not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(notice);
        }

        // ==========================
        // CREATE GET
        // ==========================
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Create()
        {
            return View();
        }

        // ==========================
        // CREATE POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return View(notice);
            }

            await _noticeService.AddAsync(notice);
            TempData["Success"] = "Notice created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // EDIT GET
        // ==========================
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int id)
        {
            var notice = await _noticeService.GetByIdAsync(id);
            if (notice == null)
            {
                TempData["Error"] = "Notice not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(notice);
        }

        // ==========================
        // EDIT POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return View(notice);
            }

            await _noticeService.UpdateAsync(notice);
            TempData["Success"] = "Notice updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // DELETE
        // ==========================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _noticeService.DeleteAsync(id);
            TempData["Success"] = "Notice deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
