using DataAccessLayer;
using System.Collections.Generic;

namespace CompanyWebAPI.Controllers
{
    public class Update
    {
        public string EmployeeRole { get; set; }
        public int EmployeeId { get; set; }
        public int CandidateId { get; set; }
        public string Comments { set; get; }
        public List<Mark> Marks { get; set; }
    }
}