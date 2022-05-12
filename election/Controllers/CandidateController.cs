using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using election.Interfaces;
using election.Models;
using election.Models.Request;
using election.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace election.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/candidates")]
    public class CandidateController : ControllerBase
    {
        //private readonly ILogger<CandidatesController> _logger;
        private readonly IDataAccessProvider _dataAccessProvider;

        public CandidateController(IDataAccessProvider dataAccessProvider)
        {
            //_logger = logger;
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public ActionResult GetAllCandidates()
        {
            var allCandidates = _dataAccessProvider.GetAllCandidates();

            return new JsonResult(allCandidates);
        }

        [HttpGet("{candidateId}")]
        public ActionResult GetCandidateDetail(int candidateId)
        {
            var candidate = _dataAccessProvider.GetCandidateById(candidateId);

            return new JsonResult(candidate);
        }

        [HttpPost]
        public ActionResult CreateCandidate(CreateCandidateRequest request)
        {
            CandidateModel model = new CandidateModel(request);
            _dataAccessProvider.AddCandidate(model);
            CreateCandidateResponse response = new CreateCandidateResponse(model);
            return new JsonResult(response);

        }

        [HttpPut("{candidateId}")]
        public ActionResult UpdateCandidateById(int candidateId, UpdateCandidateRequest request)
        {
            CandidateModel candidate = new CandidateModel(request);
            candidate = _dataAccessProvider.UpdateCandidate(candidateId, candidate);
            UpdateCandidateResponse response = new UpdateCandidateResponse(candidate);
            return new JsonResult(response);
        }

        [HttpDelete("{candidateId}")]
        public ActionResult DeleteCandidateById(int candidateId)
        {
            DeleteCandidateResponse response = new DeleteCandidateResponse();
            try
            {
                _dataAccessProvider.DeleteCandidate(candidateId);
                response.status = "ok";
            }
            catch (Exception e)
            {
                response.status = "error";
                response.message = e.Message;
            }

            return new JsonResult(response);
        }
    }
}
