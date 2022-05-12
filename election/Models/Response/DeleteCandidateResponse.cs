using System;
using System.Text.Json.Serialization;

namespace election.Models.Response
{
    public class DeleteCandidateResponse
    {
        public DeleteCandidateResponse()
        {
        }
        public string status { get; set; }
        public string message { get; set; }
    }
}
