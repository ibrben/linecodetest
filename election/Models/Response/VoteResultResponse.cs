using System;
namespace election.Models.Response
{
    public class VoteResultResponse
    {
        public VoteResultResponse()
        {
        }
        public string id { get; set; }
        public int voutedCount { get; set; }
    }
}
