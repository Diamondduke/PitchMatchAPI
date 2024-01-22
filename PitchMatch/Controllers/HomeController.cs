using Microsoft.AspNetCore.Mvc;
using PitchMatch.Data;


namespace PitchMatch.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly PitchMatchDbContext _db;

        public HomeController(PitchMatchDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetTopPitches()
        {
            // Retrieve the top 6 pitches with the most views
            var topPitches = _db.Pitch
                .OrderByDescending(p => p.Views)
                .Take(9)
                .ToList();

            return Ok(topPitches);
        }

    }
}
