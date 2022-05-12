using System;
namespace election.Models.Request
{
    public class VoteRequest
    {
        public VoteRequest()
        {
        }
        public string nationalId { get; set; }
        public int candidateId { get; set; }
    }
}
