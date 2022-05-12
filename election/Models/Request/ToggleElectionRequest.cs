using System;
namespace election.Models.Request
{
    public class ToggleElectionRequest
    {
        public ToggleElectionRequest()
        {
        }
        public bool enable { get; set; }
    }
}
