using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PitchMatch.Data;
using PitchMatch.Data.Models;
using PitchMatch.Securituy;
using System.ComponentModel.DataAnnotations;

namespace PitchMatch.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PitchMatchDbContext _db;

        public UserController(PitchMatchDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.User.ToListAsync();
            return Ok(users);
        }

        //[Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _db.User.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            var generatedSalt = PasswordHasher.GenerateSalt();

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Salt = generatedSalt,
                Password = PasswordHasher.HashPassword(user.Password + generatedSalt),
                Bio = user.Bio,
                SoMe = user.SoMe,
                ImgUrl = user.ImgUrl,
                Contact = user.Contact,
                CvUrl = user.CvUrl,
                PersonalData = null,
                Pitches = null,
                Investments = null,
                Rating = 0,
            };

            _db.User.Add(newUser);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UpdateUser user)
        {
            User? oldUser = await _db.User.FindAsync(id);

            if (oldUser == null)
            {
                return NotFound();
            }

            oldUser.Name = user.Name;
            oldUser.Password = PasswordHasher.HashPassword(user.Password + oldUser.Salt);
            oldUser.Bio = user.Bio;
            oldUser.SoMe = user.SoMe;
            oldUser.ImgUrl = user.ImgUrl;
            oldUser.Contact = user.Contact;
            oldUser.CvUrl = user.CvUrl;
            await _db.SaveChangesAsync();
            return Ok();
        }

        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User? user = await _db.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _db.User.Remove(user);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }

    public class CreateUser
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        [MaxLength(300)]
        public string Password { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string? Bio { get; set; }
        [MaxLength(300)]
        public string? Contact { get; set; }
        [MaxLength(300)]
        public string? SoMe { get; set; }
        [MaxLength(300)]
        public string? ImgUrl { get; set; }
        [MaxLength(300)]
        public string? CvUrl { get; set; }

    }

    public class UpdateUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? Contact { get; set; }
        public string? SoMe { get; set; }
        public string? ImgUrl { get; set; }
        public string? CvUrl { get; set; }

    }

}
