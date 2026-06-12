using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Web.Controllers.Api;

[Route("api/users")]
[ApiController]
public class UsersApiController : ControllerBase
{
    private readonly IUserService _service;

    public UsersApiController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppUser user)
    {
        await _service.CreateAsync(user);
        return Ok();
    }
}