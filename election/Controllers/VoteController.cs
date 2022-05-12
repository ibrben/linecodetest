using election.Exceptions;
using election.Interfaces;
using election.Models;
using election.Models.Request;
using election.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace election.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/vote")]
    public class VoteController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        private readonly IElectionState _electionState;

        public VoteController(IDataAccessProvider dataAccessProvider, IElectionState electionState)
        {
            _dataAccessProvider = dataAccessProvider;
            _electionState = electionState;
        }
        [HttpPost]
        [Route("status")]
        public IActionResult CheckVoteStatus(CheckVoteStatusRequest request)
        {
            VoteModel voted = _dataAccessProvider.GetVoteByNationalId(request.nationalId);
            CheckVoteStatusResponse response = new CheckVoteStatusResponse();
            response.status = (voted == null && _electionState.GetElectionState());
            return new JsonResult(response);
        }

        [HttpPost]
        public ActionResult SendVote(VoteRequest request)
        {
            if(!_electionState.GetElectionState())
            {
                return new JsonResult(new CommonResponse("error", "Election is not start"));
            }
            try
            {
                VoteModel vote = new VoteModel(request);
                _dataAccessProvider.Vote(vote);
            } catch (AlreadyVoteException)
            {
                return new JsonResult(new CommonResponse("error", "Already voted"));
            }
            

            return new JsonResult(new CommonResponse("ok"));
        }
    }
}
