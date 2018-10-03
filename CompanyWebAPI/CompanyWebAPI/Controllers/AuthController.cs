using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyWebAPI.DataAccessLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<TblCandidate> AllCandidate([FromBody] Auth login)
        {
            List<TblCandidate> candidates = new List<TblCandidate>();
            TblEmployee User = _context.TblEmployee.Where(x => x.EmployeeEmail == login.UserName & x.EmployeePassword == login.UserPassword).FirstOrDefault();
            if (User == null)
            {
                return null;
            }
            if (User.EmployeeRole == "IT" || User.EmployeeRole == "it")
            {
                candidates = (from i in _context.TblInterviewDetails
                              join j in _context.TblCandidate on
i.CandidateId equals j.CandidateId
                              where i.Status == 0
                              select new TblCandidate
                              {
                                  CandidateId = j.CandidateId,
                                  CandidateName = j.CandidateName,
                                  CandidateAddress = j.CandidateAddress,
                                  CandidateContactNo = j.CandidateContactNo,
                                  CandidateDateOfBirth = j.CandidateDateOfBirth,
                                  CandidateEmail = j.CandidateEmail,
                                  CandidateHighestQualification = j.CandidateHighestQualification,
                                  CandidateResume = j.CandidateResume,
                              }
                              ).ToList();

            }
            if (User.EmployeeRole == "HR" || User.EmployeeRole == "hr")
            {
                candidates = (from i in _context.TblInterviewDetails
                              join j in _context.TblCandidate on i.CandidateId equals j.CandidateId
                              where i.Status == 1
                              select new TblCandidate
                              {
                                  CandidateId = j.CandidateId,
                                  CandidateName = j.CandidateName,
                                  CandidateAddress = j.CandidateAddress,
                                  CandidateContactNo = j.CandidateContactNo,
                                  CandidateDateOfBirth = j.CandidateDateOfBirth,
                                  CandidateEmail = j.CandidateEmail,
                                  CandidateHighestQualification = j.CandidateHighestQualification,
                                  CandidateResume = j.CandidateResume,
                              }
                              ).ToList();

            }

            return candidates;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Employee([FromBody] Auth login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TblEmployee tblEmployee =  _context.TblEmployee.Where(x => x.EmployeeEmail == login.UserName & x.EmployeePassword == login.UserPassword).FirstOrDefault();

            if (tblEmployee == null)
            {
                return NotFound();
            }

            return Ok(tblEmployee);
        }
    }
    }