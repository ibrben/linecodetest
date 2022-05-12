using System;
using AutoFixture;
using FluentAssertions;
using election.Controllers;
using election.DAO;
using election.Models.Request;
using election.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using election.Interfaces;

namespace election.Tests.Controllers
{
    public class ElectionControllerTest
    {
        private ElectionController _controller;
        private Mock<IDataAccessProvider> _dataAccessProvider;
        private Mock<IElectionState> _electionState;

        private readonly IFixture fixture = new Fixture();
        [SetUp]
        public void Setup()
        {
            _dataAccessProvider = new Mock<IDataAccessProvider>();
            _electionState = new Mock<IElectionState>();

            _controller = new ElectionController(_dataAccessProvider.Object, _electionState.Object);
        }

        #region ToggleElection
        [Test]
        public void StartElection()
        {
            ToggleElectionRequest req = fixture.Build<ToggleElectionRequest>()
                .With(e => e.enable, true)
                .Create();

            _electionState.Setup(e => e.GetElectionState()).Returns(true);

            var result = _controller.ToggleElection(req);

            result.Should().BeOfType<JsonResult>().Which.Value.Should().BeOfType<ToggleElectionResponse>();

            var toggleElection = result.As<JsonResult>().Value.As<ToggleElectionResponse>();

            Assert.AreEqual("ok", toggleElection.status);
            Assert.AreEqual(true, toggleElection.enable);
        }
        [Test]
        public void StopElection()
        {
            ToggleElectionRequest req = fixture.Build<ToggleElectionRequest>()
                .With(e => e.enable, false)
                .Create();

            _electionState.Setup(e => e.GetElectionState()).Returns(false);

            var result = _controller.ToggleElection(req);

            result.Should().BeOfType<JsonResult>().Which.Value.Should().BeOfType<ToggleElectionResponse>();

            var toggleElection = result.As<JsonResult>().Value.As<ToggleElectionResponse>();

            Assert.AreEqual("ok", toggleElection.status);
            Assert.AreEqual(false, toggleElection.enable);
        }
        #endregion
    }
}
