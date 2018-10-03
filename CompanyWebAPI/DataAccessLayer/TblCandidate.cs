using System;
using System.Collections.Generic;

namespace CompanyWebAPI.DataAccessLayer
{
    public partial class TblCandidate
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public string CandidateAddress { get; set; }
        public string CandidateHighestQualification { get; set; }
        public string CandidateContactNo { get; set; }
        public string CandidateResume { get; set; }
        public string CandidateDateOfBirth { get; set; }
    }
}
