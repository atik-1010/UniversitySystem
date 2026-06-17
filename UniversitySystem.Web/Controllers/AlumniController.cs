using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs;

namespace UniversitySystem.Web.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class AlumniController : Controller
    {
        private readonly IAlumniService _alumniService;
        private readonly IDepartmentService _departmentService;

        public AlumniController(IAlumniService alumniService, IDepartmentService departmentService)
        {
            _alumniService = alumniService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var alumni = await _alumniService.GetAllAsync();
            return View(alumni);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await _departmentService.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlumniDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(await _departmentService.GetAllAsync(), "Id", "Name", dto.DepartmentId);
                return View(dto);
            }

            await _alumniService.CreateAsync(dto);
            TempData["Success"] = "Alumni Added";
            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // EDIT GET
        // ==========================
        public async Task<IActionResult> Edit(int id)
        {
            var alumni = await _alumniService.GetByIdAsync(id);
            if (alumni == null) return NotFound();

            ViewBag.Departments = new SelectList(await _departmentService.GetAllAsync(), "Id", "Name", alumni.DepartmentId);
            return View(alumni);
        }

        // ==========================
        // EDIT POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AlumniDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(await _departmentService.GetAllAsync(), "Id", "Name", dto.DepartmentId);
                return View(dto);
            }

            await _alumniService.EditAsync(dto);
            TempData["Success"] = "Alumni Updated";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var alumni = await _alumniService.GetByIdAsync(id);
            if (alumni == null) return NotFound();
            return View(alumni);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _alumniService.DeleteAsync(id);
            TempData["Success"] = "Alumni Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
