using CompanyWebAPI.DataAccessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IEnumerable<TblInterviewDetails> GetInterviewDetails()
        {
            return _context.TblInterviewDetails;
        }

        // GET: api/InterviewDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInterviewDetails([FromRoute] int id)
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
        public async Task<IActionResult> PutInterviewDetails([FromRoute] int id, [FromBody] Update details)
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
                tblInterviewDetails.Comments = details.Comments;
            }
            if (details.EmployeeRole == "IT" || details.EmployeeRole == "it")
            {
                tblInterviewDetails.Itinterviewer = details.EmployeeId;
                tblInterviewDetails.Status = tblInterviewDetails.Status + 1;
                foreach (Mark mark in details.Marks)
                {
                    _context.TblSkills.Add(new TblSkills
                    {
                        CandidateId = details.CandidateId,
                        CourseId = mark.CourseID,
                        Marks = mark.Marks
                    });
                }
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
        public async Task<IActionResult> PostInterviewDetails([FromBody] TblInterviewDetails tblInterviewDetails)
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
        public async Task<IActionResult> DeleteInterviewDetails([FromRoute] int id)
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