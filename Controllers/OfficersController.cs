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
    public class OfficersController : ControllerBase
    {
        private readonly EPoliceDbContext _context;

        public OfficersController(EPoliceDbContext context)
        {
            _context = context;
        }

        // GET: api/Officers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Officer>>> GetOfficers()
        {
            return await _context.Officers.ToListAsync();
        }

        // GET: api/Officers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Officer>> GetOfficer(int id)
        {
            var officer = await _context.Officers.FindAsync(id);

            if (officer == null)
            {
                return NotFound();
            }

            return officer;
        }

        // PUT: api/Officers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfficer(int id, Officer officer)
        {
            if (id != officer.OfficerId)
            {
                return BadRequest();
            }

            _context.Entry(officer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Officers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Officer>> PostOfficer(Officer officer)
        {
            _context.Officers.Add(officer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfficer", new { id = officer.OfficerId }, officer);
        }

        // DELETE: api/Officers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficer(int id)
        {
            var officer = await _context.Officers.FindAsync(id);
            if (officer == null)
            {
                return NotFound();
            }

            _context.Officers.Remove(officer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfficerExists(int id)
        {
            return _context.Officers.Any(e => e.OfficerId == id);
        }
    }
}
