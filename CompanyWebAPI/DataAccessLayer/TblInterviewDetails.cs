using System;
using System.Collections.Generic;

namespace CompanyWebAPI.DataAccessLayer
{
    public partial class TblInterviewDetails
    {
        public int ApplicationId { get; set; }
        public int CandidateId { get; set; }
        public int Itinterviewer { get; set; }
        public int Hrinterviewer { get; set; }
        public int Status { get; set; }

        public TblEmployee HrinterviewerNavigation { get; set; }
    }
}
