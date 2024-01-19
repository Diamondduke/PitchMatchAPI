using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PitchMatch.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PitchMatch.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest, User user)
        {
            //your logic for login process
            //If login username and password are correct then proceed to generate token
            if (loginRequest.Email != user.Email && loginRequest.Password != user.Password)
            {
                return Unauthorized();
            }
            if (loginRequest.Email != user.Email || loginRequest.Password != user.Password)
            {
                return Unauthorized();
            }
            if (loginRequest.Email == user.Email && loginRequest.Password == user.Password)
            {
               
              var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
              var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

             var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
            }
            return BadRequest();
        }
    }
}