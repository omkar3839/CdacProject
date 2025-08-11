using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using EPoliceConnectAPI.Models;
using EPoliceConnectAPI.DTOs;

namespace EPoliceConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CivilianController : ControllerBase
    {
        private readonly EPoliceDbContext _context;
        private readonly IConfiguration _configuration;

        public CivilianController(EPoliceDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(Civilian civilian)
        {
            _context.Civilians.Add(civilian);
            await _context.SaveChangesAsync();
            return Ok(civilian);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CivilianLoginDTO loginData)
        {
            var civilian = await _context.Civilians
                .FirstOrDefaultAsync(c => c.Email == loginData.Email && c.Password == loginData.Password);

            if (civilian == null)
                return Unauthorized("Invalid credentials");

            // Create claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, civilian.Name),
                new Claim("civilianId", civilian.CivilianId.ToString()), // custom claim
                new Claim(ClaimTypes.Role, "Civilian")
            };

            // Create JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                CivilianId = civilian.CivilianId,
                FullName = civilian.Name
            });
        }
    }
}
