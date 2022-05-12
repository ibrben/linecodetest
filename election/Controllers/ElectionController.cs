using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using election.Interfaces;
using election.Models;
using election.Models.Request;
using election.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace election.Controllers
{
    [ApiController]
    [Route("api/election")]
    public class ElectionController : ControllerBase
    {
        IDataAccessProvider _dataAccessProvicer;
        IElectionState _electionState;
        public ElectionController(IDataAccessProvider dataAccessProvider, IElectionState electionState)
        {
            _dataAccessProvicer = dataAccessProvider;
            _electionState = electionState;

        }
        [HttpPost]
        [Route("toggle")]
        public ActionResult ToggleElection(ToggleElectionRequest request)
        {
            _electionState.ToggleElection(request.enable);

            bool status = _electionState.GetElectionState();

            return new JsonResult(new ToggleElectionResponse("ok",status));
        }

        [HttpGet]
        [Route("result")]
        public ActionResult GetElectionResult()
        {
            List<VoteResultResponse> response = new List<VoteResultResponse>();
            var candidates = _dataAccessProvicer.GetAllCandidates();
            foreach(CandidateModel candidate in candidates) {
                VoteResultResponse vote = new VoteResultResponse();
                vote.id = candidate.id.ToString();
                vote.voutedCount = candidate.voteCount;
                response.Add(vote);
            }
            return new JsonResult(response);
        }

        [HttpGet]
        [Route("export")]
        public ActionResult ExportElectionResult()
        {
            List<CandidateModel> candidates = _dataAccessProvicer.GetAllCandidates();

            var builder = new StringBuilder();
            builder.AppendLine("id,vouteCounte");
            foreach(var candidate in candidates)
            {
                builder.AppendLine($"{candidate.id},{candidate.voteCount}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()),"text/csv","result.csv");
        }
    }
}
