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
            PersonalData? userPersonalData = await _db.PersonalData.Where(p=>p.UserId == id).FirstOrDefaultAsync(); 
            if (userPersonalData == null)
            {
                return NotFound();
            }
            return Ok(userPersonalData);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonalData(int id, CreatePersonalData newPersonalData)
        {
            User? userPersonalData = await _db.User.FindAsync(id);

            if (userPersonalData == null)
            {
                return NotFound();
            }
            userPersonalData.PersonalData = new PersonalData
            {
                PhoneNumber = newPersonalData.PhoneNumber,
                PersonNr = newPersonalData.PersonNr,
                Address = newPersonalData.Address,
                IsVerified =newPersonalData.IsVerified,
                UserId = newPersonalData.UserId
            };

            await _db.SaveChangesAsync();

            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDataPersonal(int userId,CreatePersonalData user)
        {
            User? userPersonalData = await _db.User.FindAsync(userId);

            if (userPersonalData == null)
            {
                return NotFound();
            }

           userPersonalData.PersonalData = new PersonalData
                {
                    PhoneNumber = user.PhoneNumber,
                    PersonNr = user.PersonNr,
                    Address = user.Address,
                    IsVerified = user.IsVerified,
                    UserId = userId
                };

            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePersonal(int userId)
        {
            User? userPersonalData = await _db.User.FindAsync(userId);

            if (userPersonalData == null)
            {
                return NotFound();
            }
            userPersonalData.PersonalData = null;

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
       
        [MinLength(20, ErrorMessage = "Address must be at least 20 digits.")]
        public string? Address { get; set; }

        public bool IsVerified { get; set; }

        public int UserId { get; set; }
    }

