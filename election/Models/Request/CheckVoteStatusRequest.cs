using System;
namespace election.Models.Request
{
    public class CheckVoteStatusRequest
    {
        public CheckVoteStatusRequest()
        {
        }
        public string nationalId { get; set; }
    }
}
