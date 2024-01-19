using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PitchMatch.Data;
using PitchMatch.Data.Models;
using PitchMatch.Securituy;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PitchMatch.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly PitchMatchDbContext _db;
        public LoginController(IConfiguration config, PitchMatchDbContext db)
        {
            _config = config;
            _db = db;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            //your logic for login process
            //If login username and password are correct then proceed to generate token

            var user = _db.User.Where(u => u.Email.ToLower() == loginRequest.Email.ToLower()).FirstOrDefault();

            if (user is null) //Check if user with that email is found
            {
                return Unauthorized();
            }
            else //if user is found, hash login request and check against user password
            {
                var hashedPassword = PasswordHasher.HashPassword(loginRequest.Password + user.Salt);

                if (user.Password !=  hashedPassword)
                {
                    return Unauthorized();
                }
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
             _config["Jwt:Issuer"],
             null,
             expires: DateTime.Now.AddMinutes(120),
             signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(new { 
            AccessToken=token,
            ExpiresIn=Sectoken.ValidTo,
            UserId=user.Id
            });
        }
    }
}