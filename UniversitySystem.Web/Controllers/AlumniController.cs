using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Application.DTOs;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Web.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student")]
    public class AlumniController : Controller
    {
        private readonly IAlumniService _alumniService;

        public AlumniController(IAlumniService alumniService)
        {
            _alumniService = alumniService;
        }

        // ==========================
        // INDEX (All Alumni)
        // ==========================
        public async Task<IActionResult> Index()
        {
            var alumni = await _alumniService.GetAllAsync();
            return View(alumni); // typed List<AlumniDto>
        }

        // ==========================
        // DETAILS
        // ==========================
        public async Task<IActionResult> Details(int id)
        {
            var alum = await _alumniService.GetByIdAsync(id);
            if (alum == null)
            {
                TempData["Error"] = "Alumni not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(alum);
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
        public async Task<IActionResult> Create(Alumni alumni)
        {
            if (!ModelState.IsValid)
            {
                return View(alumni);
            }

            await _alumniService.AddAsync(alumni);
            TempData["Success"] = "Alumni record created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // EDIT GET
        // ==========================
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int id)
        {
            var alum = await _alumniService.GetByIdAsync(id);
            if (alum == null)
            {
                TempData["Error"] = "Alumni not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(alum);
        }

        // ==========================
        // EDIT POST
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(Alumni alumni)
        {
            if (!ModelState.IsValid)
            {
                return View(alumni);
            }

            await _alumniService.UpdateAsync(alumni);
            TempData["Success"] = "Alumni record updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // DELETE
        // ==========================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _alumniService.DeleteAsync(id);
            TempData["Success"] = "Alumni record deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
