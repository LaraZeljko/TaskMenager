using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TaskMenager.Models;
using TaskMenager.Data;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly TaskManagerContext _context;

    public UsersController(TaskManagerContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(_context.Users.ToList());
    }
}
