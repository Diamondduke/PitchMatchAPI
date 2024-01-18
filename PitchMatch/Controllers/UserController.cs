using Microsoft.AspNetCore.Mvc;
using PitchMatch.Data;
using PitchMatch.Data.Models;

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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int userId)
        {
            var user= await _db.User.FindAsync(userId);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Bio=user.Bio,
                SoMe=user.SoMe,
                ImgUrl=user.ImgUrl,
                Contact=user.Contact,
                CvUrl=user.CvUrl,
            };

            _db.User.Add(newUser);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {id=newUser.Id},newUser);
        }

    }

    public class CreateUser
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? Contact { get; set; }
        public string? SoMe { get; set; }
        public string? ImgUrl { get; set; }
        public string? CvUrl { get; set; }

    }
}
