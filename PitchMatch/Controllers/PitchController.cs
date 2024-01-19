using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PitchMatch.Data;
using PitchMatch.Data.Models;

namespace PitchMatch.Controllers
{
    
    [Route("/[controller]")]
    [ApiController]
    public class PitchController : ControllerBase
    {
        private readonly PitchMatchDbContext _db;

        public PitchController(PitchMatchDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPitches()
        {
            var pitches = await _db.Pitch.ToListAsync();
            return Ok(pitches);
        }
        //[Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPitch(int pitchId)
        {
            var pitch = await _db.Pitch.FindAsync(pitchId);
            if(pitch == null)
            {
                return NotFound();
            }
            return Ok(pitch);
        }
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePitch(CreatePitch pitch)
        {
            Pitch newPitch = new Pitch
            {
                Title = pitch.Title,
                Summary = pitch.Summary,
                Description = pitch.Description,
                ImgUrl = pitch.ImgUrl,
                VideoUrl = pitch.VideoUrl,
                Location = pitch.Location,
                Goal = pitch.Goal,
                Yield = pitch.Yield,
                Categories = pitch.Categories,
                UserId = pitch.UserId,
            };

            _db.Add(newPitch);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPitch), new { id = newPitch.Id }, newPitch);
        }
        //[Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePitch(int pitchId, CreatePitch pitch)
        {
            var oldPitch = await _db.Pitch.FindAsync(pitchId);
            if(oldPitch == null)
            {
                return NotFound();
            }

            oldPitch.Title = pitch.Title;
            oldPitch.Summary = pitch.Summary;
            oldPitch.Description = pitch.Description;
            oldPitch.ImgUrl = pitch.ImgUrl;
            oldPitch.VideoUrl = pitch.VideoUrl;
            oldPitch.Location = pitch.Location;
            oldPitch.Goal = pitch.Goal;
            oldPitch.Yield = pitch.Yield;
            oldPitch.Categories = pitch.Categories;

            await _db.SaveChangesAsync();
            return NoContent();
        }
        //[Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePitch(int pitchId)
        {
            var pitch = await _db.Pitch.FindAsync(pitchId);
            if(pitch == null)
            {
                return NotFound();
            }

            _db.Pitch.Remove(pitch);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }

    public class CreatePitch
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImgUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal Goal { get; set; }
        public decimal Yield { get; set; }
        public string Categories { get; set; } = string.Empty;
    }
}
