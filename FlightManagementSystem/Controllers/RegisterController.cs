using FlightManagementSystem.Data;
using FlightManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(Users user)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    return BadRequest("User already exist!.");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Account successfully created." });
            }

            return BadRequest("Incorrect input.");
        }
    }
}
