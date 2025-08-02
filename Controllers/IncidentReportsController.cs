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
    public class IncidentReportsController : ControllerBase
    {
        private readonly EPoliceDbContext _context;

        public IncidentReportsController(EPoliceDbContext context)
        {
            _context = context;
        }

        // GET: api/IncidentReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentReport>>> GetIncidentReports()
        {
            return await _context.IncidentReports.ToListAsync();
        }

        // GET: api/IncidentReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentReport>> GetIncidentReport(int id)
        {
            var incidentReport = await _context.IncidentReports.FindAsync(id);

            if (incidentReport == null)
            {
                return NotFound();
            }

            return incidentReport;
        }

        // PUT: api/IncidentReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidentReport(int id, IncidentReport incidentReport)
        {
            if (id != incidentReport.ReportId)
            {
                return BadRequest();
            }

            _context.Entry(incidentReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentReportExists(id))
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

        // POST: api/IncidentReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IncidentReport>> PostIncidentReport(IncidentReport incidentReport)
        {
            _context.IncidentReports.Add(incidentReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidentReport", new { id = incidentReport.ReportId }, incidentReport);
        }

        // DELETE: api/IncidentReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidentReport(int id)
        {
            var incidentReport = await _context.IncidentReports.FindAsync(id);
            if (incidentReport == null)
            {
                return NotFound();
            }

            _context.IncidentReports.Remove(incidentReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncidentReportExists(int id)
        {
            return _context.IncidentReports.Any(e => e.ReportId == id);
        }
    }
}
