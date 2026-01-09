using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TaskMenager.Data;
using TaskMenager.Models;
using TaskMenager.DTOs;

namespace TaskMenager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TaskManagerContext _context;

        public AuthController(TaskManagerContext context)
        {
            _context = context;
        }

        // ---------------- LOGIN ----------------
        [HttpPost("login")]
        public IActionResult Login(User loginUser)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == loginUser.Username
                                  && u.Password == loginUser.Password);

            if (user == null)
                return Unauthorized("Invalid username or password");

            return Ok(user);
        }

        // ---------------- REGISTER ----------------
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username i password su obavezni");
            }

            bool exists = _context.Users.Any(u => u.Username == request.Username);

            if (exists)
            {
                return BadRequest("Korisnik već postoji");
            }

            var user = new User
            {
                Username = request.Username,
                Password = request.Password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Registracija uspješna");
        }
    }
}
