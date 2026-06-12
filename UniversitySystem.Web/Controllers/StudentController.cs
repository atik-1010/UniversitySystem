using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Web.Controllers;

[Authorize(Roles = "Admin,Teacher,Student")]
public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly IDepartmentService _departmentService;

    public StudentController(
        IStudentService studentService,
        IDepartmentService departmentService)
    {
        _studentService = studentService;
        _departmentService = departmentService;
    }

    // ================= INDEX =================
    public async Task<IActionResult> Index(string search, int page = 1)
    {
        int pageSize = 5;

        var data = await _studentService.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(search))
        {
            data = data.Where(x =>
                (x.StudentIdCode != null && x.StudentIdCode.ToLower().Contains(search.ToLower())) ||
                (x.Name != null && x.Name.ToLower().Contains(search.ToLower())) ||
                (x.Email != null && x.Email.ToLower().Contains(search.ToLower())) ||
                (x.DepartmentName != null && x.DepartmentName.ToLower().Contains(search.ToLower())))
                .ToList();
        }

        var pagedData = data
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.Search = search;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(data.Count / (double)pageSize);

        return View(pagedData);
    }

    // ================= CREATE (GET) =================
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Create()
    {
        var departments = await _departmentService.GetAllAsync();

        ViewBag.TotalDepartment = departments.Count;
        ViewBag.Departments = new SelectList(departments, "Id", "Name");

        return View();
    }

    // ================= CREATE (POST) =================
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Create(Student student)
    {
        var departments = await _departmentService.GetAllAsync();

        ViewBag.TotalDepartment = departments.Count;
        ViewBag.Departments = new SelectList(departments, "Id", "Name", student.DepartmentId);

        // 🔥 basic validation safety
        if (string.IsNullOrWhiteSpace(student.StudentIdCode))
            ModelState.AddModelError(nameof(student.StudentIdCode), "Required");

        if (string.IsNullOrWhiteSpace(student.Name))
            ModelState.AddModelError(nameof(student.Name), "Required");

        if (string.IsNullOrWhiteSpace(student.Email))
            ModelState.AddModelError(nameof(student.Email), "Required");

        if (student.DepartmentId <= 0)
            ModelState.AddModelError(nameof(student.DepartmentId), "Select Department");

        if (!ModelState.IsValid)
            return View(student);

        await _studentService.AddAsync(student);

        TempData["Success"] = "Student Created Successfully";

        return RedirectToAction(nameof(Index));
    }

    // ================= EDIT (GET) =================
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Edit(int id)
    {
        var student = await _studentService.GetByIdAsync(id);

        if (student == null)
            return NotFound();

        var departments = await _departmentService.GetAllAsync();

        ViewBag.Departments = new SelectList(
            departments,
            "Id",
            "Name",
            student.DepartmentId
        );

        return View(student);
    }

    // ================= EDIT (POST) =================
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Edit(Student student)
    {
        var departments = await _departmentService.GetAllAsync();

        ViewBag.Departments = new SelectList(
            departments,
            "Id",
            "Name",
            student.DepartmentId
        );

        if (!ModelState.IsValid)
            return View(student);

        await _studentService.UpdateAsync(student);

        TempData["Success"] = "Student Updated Successfully";

        return RedirectToAction(nameof(Index));
    }

    // ================= DELETE =================
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);

        TempData["Success"] = "Student Deleted Successfully";

        return RedirectToAction(nameof(Index));
    }
}