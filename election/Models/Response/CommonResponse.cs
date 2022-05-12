using System;
using System.Text.Json.Serialization;

namespace election.Models.Response
{
    public class CommonResponse
    {
        public CommonResponse()
        {
        }
        public CommonResponse(string status)
        {
            this.status = status;
        }
        public CommonResponse(string status, string message)
        {
            this.status = status;
            this.message = message;
        }

        public string status { get; }
        public string message { get; }
    }
}
