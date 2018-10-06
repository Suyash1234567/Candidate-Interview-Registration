using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyWebAPI.DataAccessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Cors;

namespace CompanyWebAPI.Controllers
{
    [EnableCors("CORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationsController : ControllerBase
    {
        private readonly ApnaCompanyContext _context;

        public QualificationsController()
        {
            _context = new ApnaCompanyContext();
        }

        // GET: api/Qualifications
        [HttpGet]
        public IEnumerable<TblQualification> GetTblQualifications()
        {
            return _context.TblQualifications;
        }

        // GET: api/Qualifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTblQualification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblQualification = await _context.TblQualifications.FindAsync(id);

            if (tblQualification == null)
            {
                return NotFound();
            }

            return Ok(tblQualification);
        }

        // PUT: api/Qualifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblQualification([FromRoute] int id, [FromBody] TblQualification tblQualification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblQualification.QualificationID)
            {
                return BadRequest();
            }

            _context.Entry(tblQualification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblQualificationExists(id))
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

        // POST: api/Qualifications
        [HttpPost]
        public async Task<IActionResult> PostTblQualification([FromBody] TblQualification tblQualification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TblQualifications.Add(tblQualification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblQualification", new { id = tblQualification.QualificationID }, tblQualification);
        }

        // DELETE: api/Qualifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblQualification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tblQualification = await _context.TblQualifications.FindAsync(id);
            if (tblQualification == null)
            {
                return NotFound();
            }

            _context.TblQualifications.Remove(tblQualification);
            await _context.SaveChangesAsync();

            return Ok(tblQualification);
        }

        private bool TblQualificationExists(int id)
        {
            return _context.TblQualifications.Any(e => e.QualificationID == id);
        }
    }
}