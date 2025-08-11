using EPoliceConnectAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPoliceConnectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly EPoliceDbContext _context;

        public ComplaintsController(EPoliceDbContext context)
        {
            _context = context;
        }

        // GET: api/Complaints (Police only)
        [Authorize(Roles = "Police")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
        {
            return await _context.Complaints.ToListAsync();
        }

        // GET: api/Complaints/by-civilian/{civilianId} (Civilian only)
        [Authorize(Roles = "Civilian")]
        [HttpGet("by-civilian/{civilianId}")]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaintsByCivilian(int civilianId)
        {
            var complaints = await _context.Complaints
                .Where(c => c.CivilianId == civilianId)
                .OrderByDescending(c => c.DateFiled)
                .ToListAsync();

            return Ok(complaints);
        }

        // PUT: api/Complaints/5/status (Designated Officer only)
        [Authorize(Roles = "Designated")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateComplaintStatus(int id, [FromBody] string newStatus)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }

            complaint.Status = newStatus;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Civilian")]
        public async Task<ActionResult<Complaint>> PostComplaint(Complaint complaint)
        {
            try
            {
                // Set complaint filed date to current date and time
                complaint.DateFiled = DateTime.Now;

                _context.Complaints.Add(complaint);
                await _context.SaveChangesAsync();

                return Ok(complaint);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }



        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.ComplaintId == id);
        }
    }
}
