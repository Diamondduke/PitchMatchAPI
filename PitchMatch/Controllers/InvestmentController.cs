using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PitchMatch.Data;
using PitchMatch.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PitchMatch.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly PitchMatchDbContext _db;

        public InvestmentController(PitchMatchDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvestment([FromBody] Investment investment)
        {
            var pitch = await _db.Pitch.FirstOrDefaultAsync(p => p.Id == investment.PitchId);
            if (pitch == null)
            {
                return NotFound("Pitch not found.");
            }

            pitch.Funding += investment.Amount;

            _db.Investment.Add(investment);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvestment), new { id = investment.Id }, investment);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvestment(int id)
        {
            var investment = await _db.Investment
                                      .Include(i => i.User)
                                      .Include(i => i.Pitch)
                                      .FirstOrDefaultAsync(i => i.Id == id);
            if (investment == null)
            {
                return NotFound();
            }

            return Ok(investment);
        }

        [HttpGet("pitch/{pitchId:int}")]
        public async Task<IActionResult> GetInvestmentsForPitch(int pitchId)
        {
            var investments = await _db.Investment
                                       .Where(i => i.PitchId == pitchId)
                                       .ToListAsync();
            if (investments.Count == 0)
            {
                return NotFound();
            }
            return Ok(investments);
            
        }
    }
}