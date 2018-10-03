using System;
using System.Collections.Generic;

namespace CompanyWebAPI.DataAccessLayer
{
    public partial class TblEmployee
    {
        public TblEmployee()
        {
            TblInterviewDetails = new HashSet<TblInterviewDetails>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeRole { get; set; }
        public string EmployeePassword { get; set; }

        public ICollection<TblInterviewDetails> TblInterviewDetails { get; set; }
    }
}