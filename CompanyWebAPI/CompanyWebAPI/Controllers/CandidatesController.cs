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
    public class CandidatesController : ControllerBase
    {
        private readonly ApnaCompanyContext _context;

        public CandidatesController()
        {
            _context = new ApnaCompanyContext();
        }

        // GET: api/Candidates
        [HttpGet]
        public IEnumerable<TblCandidate> Get()
        {
            return _context.TblCandidate;
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblCandidate = await _context.TblCandidate.FindAsync(id);

            if (tblCandidate == null)
            {
                return NotFound();
            }

            return Ok(tblCandidate);
        }

        // PUT: api/Candidates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCandidate([FromRoute] string id, [FromBody] TblCandidate tblCandidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCandidate.CandidateEmail)
            {
                return BadRequest();
            }

            _context.Entry(tblCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCandidateExists(id))
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

        // POST: api/Candidates
        [HttpPost] 
        public async Task<IActionResult> PostTblCandidate([FromBody] TblCandidate tblCandidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TblCandidate isUser = _context.TblCandidate.Where(x => x.CandidateEmail == tblCandidate.CandidateEmail).FirstOrDefault();

            if(isUser != null)
            {
                return Ok("Email already exist");
            }
            _context.TblCandidate.Add(tblCandidate);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblCandidateExists(tblCandidate.CandidateEmail))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
             isUser = _context.TblCandidate.Where(x => x.CandidateEmail == tblCandidate.CandidateEmail).FirstOrDefault();
            TblInterviewDetails tblInterviewDetails = new TblInterviewDetails();
            tblInterviewDetails.CandidateId = isUser.CandidateId;
            tblInterviewDetails.Hrinterviewer = 1;
            tblInterviewDetails.Itinterviewer = 1;
            _context.TblInterviewDetails.Add(tblInterviewDetails);
            _context.SaveChanges();
            return Ok("Register Successful");
        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCandidate([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblCandidate = await _context.TblCandidate.FindAsync(id);
            if (tblCandidate == null)
            {
                return NotFound();
            }

            _context.TblCandidate.Remove(tblCandidate);
            await _context.SaveChangesAsync();

            return Ok(tblCandidate);
        }

        private bool TblCandidateExists(string id)
        {
            return _context.TblCandidate.Any(e => e.CandidateEmail == id);
        }
    }
}