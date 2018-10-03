 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyWebAPI.DataAccessLayer;
using Microsoft.AspNetCore.Cors;

namespace CompanyWebAPI.Controllers
{
    [EnableCors("CORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewDetailsController : ControllerBase
    {
        private readonly ApnaCompanyContext _context;

        public InterviewDetailsController()
        {
            _context = new ApnaCompanyContext();
        }

        // GET: api/InterviewDetails
        [HttpGet]
        public IEnumerable<TblInterviewDetails> GetTblInterviewDetails()
        {
            return _context.TblInterviewDetails;
        }

        // GET: api/InterviewDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTblInterviewDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblInterviewDetails = await _context.TblInterviewDetails.FindAsync(id);

            if (tblInterviewDetails == null)
            {
                return NotFound();
            }

            return Ok(tblInterviewDetails);
        }

        // PUT: api/InterviewDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblInterviewDetails([FromRoute] int id, [FromBody] Update details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != details.CandidateId)
            {
                return BadRequest();
            }
            TblInterviewDetails tblInterviewDetails = _context.TblInterviewDetails.Where(x => x.CandidateId == details.CandidateId).FirstOrDefault();

            if (details.EmployeeRole == "HR" || details.EmployeeRole == "hr")
            {
                tblInterviewDetails.Hrinterviewer = details.EmployeeId;
                tblInterviewDetails.Status = tblInterviewDetails.Status + 1;
            }
            if (details.EmployeeRole == "IT" || details.EmployeeRole == "it")
            {
                tblInterviewDetails.Itinterviewer = details.EmployeeId;
                tblInterviewDetails.Status = tblInterviewDetails.Status + 1;
            }
            _context.Entry(tblInterviewDetails).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblInterviewDetailsExists(id))
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

        // POST: api/InterviewDetails
        [HttpPost]
        public async Task<IActionResult> PostTblInterviewDetails([FromBody] TblInterviewDetails tblInterviewDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TblInterviewDetails.Add(tblInterviewDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblInterviewDetails", new { id = tblInterviewDetails.ApplicationId }, tblInterviewDetails);
        }

        // DELETE: api/InterviewDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblInterviewDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblInterviewDetails = await _context.TblInterviewDetails.FindAsync(id);
            if (tblInterviewDetails == null)
            {
                return NotFound();
            }

            _context.TblInterviewDetails.Remove(tblInterviewDetails);
            await _context.SaveChangesAsync();

            return Ok(tblInterviewDetails);
        }

        private bool TblInterviewDetailsExists(int id)
        {
            return _context.TblInterviewDetails.Any(e => e.ApplicationId == id);
        }
    }
}