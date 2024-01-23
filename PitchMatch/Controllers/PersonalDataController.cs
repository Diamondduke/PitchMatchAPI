using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PitchMatch.Data;
using PitchMatch.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace PitchMatch.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PersonalDataController : ControllerBase
    {
        private readonly PitchMatchDbContext _db;

        public PersonalDataController(PitchMatchDbContext db)
        {
            _db = db;
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPersonalData(int id)
        {
            PersonalData? personalData = await _db.PersonalData.Where(p => p.UserId == id).FirstOrDefaultAsync();
            if (personalData == null)
            {
                return NotFound();
            }
            return Ok(personalData);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonalData(CreatePersonalData newPersonalData)
        {
            User? user = await _db.User.FindAsync(newPersonalData.UserId);

            if (user == null)
            {
                return NotFound();
            }
            user.PersonalData = new PersonalData
            {
                PhoneNumber = newPersonalData.PhoneNumber,
                PersonNr = newPersonalData.PersonNr,
                Address = newPersonalData.Address,
                Longitude = newPersonalData.Longitude,
                Latitude = newPersonalData.Latitude,
                IsVerified = newPersonalData.IsVerified,
                UserId = newPersonalData.UserId
            };

            await _db.SaveChangesAsync();

            return Ok(user.PersonalData);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDataPersonal(CreatePersonalData personalData)
        {
            PersonalData? data = await _db.PersonalData.Where((d) => d.UserId == personalData.UserId).FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            data = new PersonalData
            {
                PhoneNumber = personalData.PhoneNumber,
                PersonNr = personalData.PersonNr,
                Address = personalData.Address,
                Longitude = personalData.Longitude,
                Latitude = personalData.Latitude,
                IsVerified = personalData.IsVerified,
                UserId = personalData.UserId
            };

            await _db.SaveChangesAsync();
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePersonal(int userId)
        {
            PersonalData? data = await _db.PersonalData.Where((d)=> d.UserId == userId).FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            _db.PersonalData.Remove(data);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }

}

public class CreatePersonalData
{
    [MinLength(8, ErrorMessage = "Phone number must be at least 8 digits.")]
    [MaxLength(8, ErrorMessage = "Phone number must be at most 8 digits.")]
    public string? PhoneNumber { get; set; }

    [MinLength(11, ErrorMessage = "Personal number must be at least 11 digits.")]
    [MaxLength(11, ErrorMessage = "Personal number must be at most 11 digits.")]
    public string? PersonNr { get; set; }

    public string? Address { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public bool IsVerified { get; set; }

    public int UserId { get; set; }
}

