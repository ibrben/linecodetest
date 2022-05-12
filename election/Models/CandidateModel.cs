using System;
using System.ComponentModel.DataAnnotations.Schema;
using election.Models.Request;

namespace election.Models
{
    public class CandidateModel
    {
        public CandidateModel()
        {
        }
        public CandidateModel(CreateCandidateRequest candidate)
        {
            this.id = Int32.Parse(candidate.id);
            this.name = candidate.name;
            this.dob = candidate.dob;
            this.bioLink = candidate.bioLink;
            this.imageLink = candidate.imageLink;
            this.policy = candidate.policy;
        }
        public CandidateModel(UpdateCandidateRequest candidate)
        {
            this.id = Int32.Parse(candidate.id);
            this.name = candidate.name;
            this.dob = candidate.dob;
            this.bioLink = candidate.bioLink;
            this.imageLink = candidate.imageLink;
            this.policy = candidate.policy;
        }

        public int id { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("dob")]
        public string dob { get; set; }
        [Column("biolink")]
        public string bioLink { get; set; }
        [Column("imagelink")]
        public string imageLink { get; set; }
        [Column("policy")]
        public string policy { get; set; }
        [Column("votecount")]
        public int voteCount { get; set; }
    }
}
