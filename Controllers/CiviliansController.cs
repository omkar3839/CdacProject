using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EPoliceConnectAPI.Models;

namespace EPoliceConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CivilianController : ControllerBase
    {
        private readonly EPoliceDbContext _context;

        public CivilianController(EPoliceDbContext context)
        {
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(Civilian civilian)
        {
            _context.Civilians.Add(civilian);
            await _context.SaveChangesAsync();
            return Ok(civilian);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Civilian loginData)
        {
            var civilian = await _context.Civilians
                .FirstOrDefaultAsync(c => c.Email == loginData.Email && c.Password == loginData.Password);

            if (civilian == null)
                return Unauthorized("Invalid credentials");

            return Ok(civilian);
        }
    }

}
