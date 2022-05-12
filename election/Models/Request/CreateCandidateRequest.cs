using System;
namespace election.Models.Request
{
    public class CreateCandidateRequest
    {
        public CreateCandidateRequest()
        {
        }
        public string id { get; set; }
        public string name { get; set; }
        public string dob { get; set; }
        public string bioLink { get; set; }
        public string imageLink { get; set; }
        public string policy { get; set; }
    }
}
