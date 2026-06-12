using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Web.Controllers;

[Authorize(Roles = "Admin")]
public class DepartmentController : Controller
{
    private readonly IDepartmentService _service;

    public DepartmentController(IDepartmentService service)
    {
        _service = service;
    }

    // ================= INDEX =================
    public async Task<IActionResult> Index(string search)
    {
        var data = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(search))
        {
            data = data
                .Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower()))
                .ToList();
        }

        ViewBag.Search = search;

        return View(data);
    }

    // ================= CREATE (GET) =================
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // ================= CREATE (POST) =================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        // নেভিগেশন প্রোপার্টির ভ্যালিডেশন ইরর এড়ানোর জন্য এটি রিমুভ করা হলো
        ModelState.Remove("Students");

        if (string.IsNullOrWhiteSpace(department.Name))
        {
            ModelState.AddModelError("Name", "Department Name Required");
        }

        if (!ModelState.IsValid)
        {
            return View(department);
        }

        await _service.AddAsync(department);

        TempData["Success"] = "Department Created Successfully";

        return RedirectToAction(nameof(Index));
    }

    // ================= EDIT (GET) =================
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var department = await _service.GetByIdAsync(id);

        if (department == null)
            return NotFound();

        return View(department);
    }

    // ================= EDIT (POST) =================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Department department)
    {
        // সবচেয়ে গুরুত্বপূর্ণ লাইন: .NET যেন Students এর জন্য ভ্যালিডেশন আটকে না দেয়
        ModelState.Remove("Students");

        if (string.IsNullOrWhiteSpace(department.Name))
        {
            ModelState.AddModelError("Name", "Department Name Required");
        }

        if (!ModelState.IsValid)
        {
            // ভ্যালিডেশন ফেইল করলে এরর মেসেজ সহ এডিট পেজেই থাকবে
            return View(department);
        }

        // সবকিছু ঠিক থাকলে এবার ডাটাবেজে সাকসেসফুলি আপডেট হবে
        await _service.UpdateAsync(department);

        TempData["Success"] = "Department Updated Successfully";

        return RedirectToAction(nameof(Index));
    }

    // ================= DELETE =================
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        TempData["Success"] = "Department Deleted Successfully";

        return RedirectToAction(nameof(Index));
    }
}