using System;
using AutoFixture;
using FluentAssertions;
using election.Controllers;
using election.DAO;
using election.Exceptions;
using election.Models;
using election.Models.Request;
using election.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using election.Interfaces;

namespace election.Tests.Controllers
{
    public class VoteControllerTest
    {
        private VoteController _controller;
        private Mock<IDataAccessProvider> _dataAccessProvider;
        private Mock<IElectionState> _electionState;

        private readonly IFixture fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _dataAccessProvider = new Mock<IDataAccessProvider>();
            _electionState = new Mock<IElectionState>();

            _controller = new VoteController(_dataAccessProvider.Object, _electionState.Object);
        }

        #region CheckVoteStatus
        [Test]
        public void CheckVoteStatusReturnTrue()
        {
            CheckVoteStatusRequest req = fixture.Build<CheckVoteStatusRequest>()
                .With(c => c.nationalId, fixture.Create<int>().ToString())
                .Create();
            _dataAccessProvider.Setup(d => d.GetVoteByNationalId(It.IsAny<string>())).Returns((VoteModel)null);
            _electionState.Setup(e => e.GetElectionState()).Returns(true);

            var result = _controller.CheckVoteStatus(req);

            result.Should().BeOfType<JsonResult>().Which.Value.Should().BeOfType<CheckVoteStatusResponse>();

            var checkVoteResponse = result.As<JsonResult>().Value.As<CheckVoteStatusResponse>();

            Assert.AreEqual(true, checkVoteResponse.status);
        }
        [Test]
        public void CheckVoteStatusReturnFalseIfElectionIsNotStart()
        {
            CheckVoteStatusRequest req = fixture.Build<CheckVoteStatusRequest>()
                .With(c => c.nationalId, fixture.Create<int>().ToString())
                .Create();

            _dataAccessProvider.Setup(d => d.GetVoteByNationalId(It.IsAny<string>())).Returns((VoteModel) null);
            _electionState.Setup(e => e.GetElectionState()).Returns(false);

            var result = _controller.CheckVoteStatus(req);

            result.Should().BeOfType<JsonResult>().Which.Value.Should().BeOfType<CheckVoteStatusResponse>();

            var checkVoteResponse = result.As<JsonResult>().Value.As<CheckVoteStatusResponse>();

            Assert.AreEqual(false, checkVoteResponse.status);
        }
        #endregion
        #region Vote
        [Test]
        public void VoteSuccess()
        {
            VoteRequest req = fixture.Create<VoteRequest>();

            _electionState.Setup(e => e.GetElectionState()).Returns(true);

            var result = _controller.SendVote(req);

            result.Should().BeOfType<JsonResult>().Which.Value.Should().BeOfType<CommonResponse>();

            var commonResponse = result.As<JsonResult>().Value.As<CommonResponse>();

            Assert.AreEqual("ok", commonResponse.status);
            Assert.Null(commonResponse.message);
        }
        [Test]
        public void VoteFailedIfElectionNotStarted()
        {
            VoteRequest req = fixture.Create<VoteRequest>();

            _electionState.Setup(e => e.GetElectionState()).Returns(false);

            var result = _controller.SendVote(req);

            result.Should().BeOfType<JsonResult>()
                .Which.Value.Should().BeOfType<CommonResponse>();

            var commonResponse = result.As<JsonResult>().Value.As<CommonResponse>();

            Assert.AreEqual("error", commonResponse.status);
            Assert.NotNull(commonResponse.message);
        }
        [Test]
        public void VoteFailedIfAlreadVoted()
        {
            VoteRequest req = fixture.Create<VoteRequest>();
            VoteModel vote = fixture.Create<VoteModel>();
            _dataAccessProvider.Setup(d => d.Vote(It.IsAny<VoteModel>())).Throws(new AlreadyVoteException());
            _electionState.Setup(e => e.GetElectionState()).Returns(true);

            var result = _controller.SendVote(req);

            result.Should().BeOfType<JsonResult>().Which.Value.Should().BeOfType<CommonResponse>();

            var commonResponse = result.As<JsonResult>().Value.As<CommonResponse>();

            Assert.AreEqual("error", commonResponse.status);
            Assert.NotNull(commonResponse.As<CommonResponse>().message);
            


        }
        #endregion
    }
}
