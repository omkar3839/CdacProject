using EPoliceConnectAPI.DTOs;
using EPoliceConnectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly EPoliceDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(EPoliceDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login-civilian")]
    public IActionResult LoginCivilian([FromBody] CivilianLoginDTO login)
    {
        //checks for civilian table(LINQ query)
        var civilian = _context.Civilians
            .FirstOrDefault(c => c.Email == login.Email && c.Password == login.Password);

        if (civilian == null)
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(civilian);

        return Ok(new LoginResponseDTO
        {
            Token = token,
            FullName = civilian.Name,
            CivilianId = civilian.CivilianId
        });
    }

    [HttpPost("login-officer")]
    public IActionResult LoginOfficer([FromBody] OfficerLoginDTO login)
    {
        var officer = _context.Officers
            .FirstOrDefault(o => o.Email == login.Email && o.Password == login.Password);

        if (officer == null)
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(officer);

        return Ok(new OfficerLoginResponseDTO
        {
            Token = token,
            Name = officer.Name,
            OfficerId = officer.OfficerId,
            Rank = officer.Rank,
            IsDesignated = officer.IsDesignated ?? false
        });
    }

    private string GenerateJwtToken(Civilian civilian)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, civilian.Email),
        new Claim("CivilianId", civilian.CivilianId.ToString()),
        new Claim(ClaimTypes.Name, civilian.Name),
        new Claim(ClaimTypes.Role, "Civilian"), // Add this line
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateJwtToken(Officer officer)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, officer.Email),
            new Claim("OfficerId", officer.OfficerId.ToString()),
            new Claim(ClaimTypes.Name, officer.Name),
            new Claim(ClaimTypes.Role, "Police"), // Common role for all officers
            new Claim("Rank", officer.Rank),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (officer.IsDesignated == true)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Designated")); // Additional role
        }

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
