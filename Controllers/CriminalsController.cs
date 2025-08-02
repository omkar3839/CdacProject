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
    public class CriminalsController : ControllerBase
    {
        private readonly EPoliceDbContext _context;

        public CriminalsController(EPoliceDbContext context)
        {
            _context = context;
        }

        // GET: api/Criminals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Criminal>>> GetCriminals()
        {
            return await _context.Criminals.ToListAsync();
        }

        // GET: api/Criminals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Criminal>> GetCriminal(int id)
        {
            var criminal = await _context.Criminals.FindAsync(id);

            if (criminal == null)
            {
                return NotFound();
            }

            return criminal;
        }

        // PUT: api/Criminals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCriminal(int id, Criminal criminal)
        {
            if (id != criminal.CriminalId)
            {
                return BadRequest();
            }

            _context.Entry(criminal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriminalExists(id))
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

        // POST: api/Criminals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Criminal>> PostCriminal(Criminal criminal)
        {
            _context.Criminals.Add(criminal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCriminal", new { id = criminal.CriminalId }, criminal);
        }

        // DELETE: api/Criminals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriminal(int id)
        {
            var criminal = await _context.Criminals.FindAsync(id);
            if (criminal == null)
            {
                return NotFound();
            }

            _context.Criminals.Remove(criminal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CriminalExists(int id)
        {
            return _context.Criminals.Any(e => e.CriminalId == id);
        }
    }
}
