using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Web.Controllers;

public class ResultController : Controller
{
    private readonly IStudentMarkService _markService;
    private readonly IStudentService _studentService;

    public ResultController(
        IStudentMarkService markService,
        IStudentService studentService)
    {
        _markService = markService;
        _studentService = studentService;
    }

    public async Task<IActionResult> Search()
    {
        ViewBag.Students =
            await _studentService.GetAllAsync();

        return View();
    }

    public async Task<IActionResult> Index(
        int studentId)
    {
        var result =
            await _markService
                .GetStudentResultAsync(
                    studentId
                );

        if (result == null)
        {
            TempData["msg"] =
                "No Result Found";

            return RedirectToAction(
                nameof(Search)
            );
        }

        ViewBag.StudentId =
            studentId;

        return View(result);
    }

    public async Task<IActionResult> Print(
        int studentId)
    {
        var result =
            await _markService
                .GetStudentResultAsync(
                    studentId
                );

        if (result == null)
        {
            return RedirectToAction(
                nameof(Search)
            );
        }

        return View(result);
    }
}