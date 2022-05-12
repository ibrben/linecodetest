using System;
namespace election.Models.Response
{
    public class ToggleElectionResponse
    {
        public ToggleElectionResponse()
        {
        }
        public ToggleElectionResponse(string status, bool enable)
        {
            this.status = status;
            this.enable = enable;
        }
        public string status { get; set; }
        public bool enable { get; set; }
    }
}
