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

        [HttpGet("/{id:int}/portfolio")]
        public async Task<IActionResult> GetUserPortfolio(int id)
        {  
            List<Pitch>? pitches= await _db.Pitch.Where(p => p.UserId == id).ToListAsync();
            if (pitches == null)
            {
                return NotFound();
            }
            return Ok(pitches);
        }

        //[Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _db.User.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            if (ModelState.IsValid == false)
            {
                return ValidationProblem(ModelState);
            }

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
        [Required(ErrorMessage = "A name is required.")]
        [MinLength(3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        [MaxLength(100, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "A unique email is required.")]
        [MinLength(3, ErrorMessage = "Email must be between 3 and 100 characters.")]
        [MaxLength(100, ErrorMessage = "Email must be between 3 and 100 characters.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least six characters.")]
        [MaxLength(300, ErrorMessage = "We don't have space for that much password!")]
        public string Password { get; set; } = string.Empty;
        [MaxLength(2000, ErrorMessage = "A biography can be 2000 characters at most. A short and snappy introduction works best!")]
        public string? Bio { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? Contact { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? SoMe { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? ImgUrl { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? CvUrl { get; set; }

    }

    public class UpdateUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A name is required.")]
        [MinLength(3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        [MaxLength(100, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least six characters.")]
        [MaxLength(300, ErrorMessage = "We don't have space for that much password!")]
        public string Password { get; set; } = string.Empty;
        [MaxLength(2000, ErrorMessage = "A biography can be 2000 characters at most. A short and snappy introduction works best!")]
        public string? Bio { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? Contact { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? SoMe { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? ImgUrl { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? CvUrl { get; set; }

    }

}
