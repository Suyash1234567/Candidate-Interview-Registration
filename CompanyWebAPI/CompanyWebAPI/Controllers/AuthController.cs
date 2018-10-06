using CompanyWebAPI.DataAccessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyWebAPI.Controllers
{
    [EnableCors("CORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApnaCompanyContext _context;

        public AuthController()
        {
            _context = new ApnaCompanyContext();
        }

        // POST: api/Auth
        [HttpPost("[action]")]
        public IEnumerable<TblCandidate> GetAllCandidate([FromBody] Authorization login)
        {
            List<TblCandidate> candidates = new List<TblCandidate>();
            TblEmployee User = _context.TblEmployee.Where(x => x.EmployeeEmail == login.UserName & x.EmployeePassword == login.UserPassword).FirstOrDefault();
            if (User == null)
            {
                return null;
            }
            if (User.EmployeeRole.ToLower() == "it")
            {
                candidates = (from i in _context.TblInterviewDetails

                              join j in _context.TblCandidate on
i.CandidateId equals j.CandidateId

                              where i.Status == 0
                              select Candidate(j)
                              ).ToList();
            }
            if (User.EmployeeRole.ToLower() == "hr")
            {
                candidates = (from i in _context.TblInterviewDetails
                              join j in _context.TblCandidate on i.CandidateId equals j.CandidateId
                              where i.Status == 1
                              select Candidate(j)).ToList();
            }

            return candidates;
        }

        [HttpPost("[action]")]
        public ActionResult Employee([FromBody] Authorization login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TblEmployee tblEmployee = _context.TblEmployee.Where(x => x.EmployeeEmail == login.UserName & x.EmployeePassword == login.UserPassword).FirstOrDefault();

            if (tblEmployee == null)
            {
                return NotFound();
            }

            return Ok(tblEmployee);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var marks = _context.TblSkills.Where(x => x.CandidateId == id).ToList();

            if (marks == null)
            {
                return null;
            }

            return Ok(marks);
        }

        private TblCandidate Candidate(TblCandidate tblInterviewDetails)
        {
            return new TblCandidate
            {
                CandidateId = tblInterviewDetails.CandidateId,
                CandidateName = tblInterviewDetails.CandidateName,
                CandidateAddress = tblInterviewDetails.CandidateAddress,
                CandidateContactNo = tblInterviewDetails.CandidateContactNo,
                CandidateDateOfBirth = tblInterviewDetails.CandidateDateOfBirth,
                CandidateEmail = tblInterviewDetails.CandidateEmail,
                CandidateHighestQualification = tblInterviewDetails.CandidateHighestQualification,
                CandidateResume = tblInterviewDetails.CandidateResume,
            };
        }
    }
}