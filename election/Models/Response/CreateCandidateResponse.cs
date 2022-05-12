using System;
namespace election.Models.Response
{
    public class CreateCandidateResponse
    {
        public CreateCandidateResponse()
        {
        }
        public CreateCandidateResponse(CandidateModel candidate)
        {
            this.id = candidate.id.ToString();
            this.name = candidate.name;
            this.dob = candidate.dob;
            this.bioLink = candidate.bioLink;
            this.imageLink = candidate.imageLink;
            this.policy = candidate.policy;
        }

        public string id { get; set; }
        public string name { get; set; }
        public string dob { get; set; }
        public string bioLink { get; set; }
        public string imageLink { get; set; }
        public string policy { get; set; }
    }
}
