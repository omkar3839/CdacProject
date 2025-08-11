using EPoliceConnectAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace EPoliceConnectAPI.Controllers
{
    [Authorize(Roles = "Designated")]
    [ApiController]
    [Route("api/[controller]")]

    public class PrisonRecordsController : ControllerBase
    {
        private readonly EPoliceDbContext _context;

        public PrisonRecordsController(EPoliceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPrisonRecords()
        {
            var records = await _context.PrisonRecords
                .Include(p => p.Criminal)  
                .Select(p => new
                {
                    p.PrisonId,
                    p.PrisonName,
                    p.SentenceYears,
                    p.ReleaseDate,
                    CriminalName = p.Criminal != null ? p.Criminal.Name : "N/A"
                })
                .ToListAsync();

            return Ok(records);
        }



        // GET: api/PrisonRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrisonRecord>> GetPrisonRecord(int id)
        {
            var prisonRecord = await _context.PrisonRecords.FindAsync(id);

            if (prisonRecord == null)
            {
                return NotFound();
            }

            return prisonRecord;
        }

        // PUT: api/PrisonRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrisonRecord(int id, PrisonRecord prisonRecord)
        {
            if (id != prisonRecord.PrisonId)
            {
                return BadRequest();
            }

            _context.Entry(prisonRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrisonRecordExists(id))
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

        // POST: api/PrisonRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrisonRecord>> PostPrisonRecord(PrisonRecord prisonRecord)
        {
            _context.PrisonRecords.Add(prisonRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrisonRecord", new { id = prisonRecord.PrisonId }, prisonRecord);
        }

        // DELETE: api/PrisonRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrisonRecord(int id)
        {
            var prisonRecord = await _context.PrisonRecords.FindAsync(id);
            if (prisonRecord == null)
            {
                return NotFound();
            }

            _context.PrisonRecords.Remove(prisonRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrisonRecordExists(int id)
        {
            return _context.PrisonRecords.Any(e => e.PrisonId == id);
        }
    }
}
