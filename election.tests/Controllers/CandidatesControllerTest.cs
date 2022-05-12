using System;
using election.Controllers;
using election.DAO;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using election.Models;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using election.Models.Request;
using election.Models.Response;
using election.Interfaces;

namespace election.Tests.Controllers
{
    [TestFixture]
    public class CandidatesControllerTest
    {
        private CandidateController _controlelr;
        private Mock<IDataAccessProvider> _dataAccessProvider;

        private readonly IFixture fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _dataAccessProvider = new Mock<IDataAccessProvider>();

            _controlelr = new CandidateController(_dataAccessProvider.Object);
        }

        #region GetAllCandidates
        [Test]
        public void GetAllCandidatesReturnOK()
        {
            var candidates = fixture.CreateMany<CandidateModel>(5).ToList();
            _dataAccessProvider.Setup(d => d.GetAllCandidates()).Returns(candidates);

            var result = _controlelr.GetAllCandidates();
            
            result.Should().BeOfType<JsonResult>(candidates.ToString());

            var response = result.As<JsonResult>().Value.As<List<CandidateModel>>();

            Assert.AreEqual(5, response.Count);
            Assert.IsNotNull(result);
            
        }
        #endregion
        #region GetCandidateDetail
        [Test]
        public void GetCandidateById()
        {
            var candidate = fixture.Build<CandidateModel>()
                .With(c => c.id, fixture.Create<int>())
                .Create();
            _dataAccessProvider.Setup(d => d.GetCandidateById(It.IsAny<int>())).Returns(candidate);

            var result = _controlelr.GetCandidateDetail(It.IsAny<int>());

            result.Should().BeOfType<JsonResult>()
                .And.Should().NotBeNull()
                .And.Should().Equals(candidate);

            var response = result.As<JsonResult>().Value.As<CandidateModel>();

            Assert.AreEqual(candidate.id, response.id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetCandidateDetailNotFound()
        {
            _dataAccessProvider.Setup(d => d.GetCandidateById(It.IsAny<int>())).Returns((CandidateModel)null);

            var result = _controlelr.GetCandidateDetail(It.IsAny<int>());

            result.Should().BeOfType<JsonResult>()
                .Which.Value.Should().BeNull();

            var response = result.As<JsonResult>().Value.As<CandidateModel>();

            Assert.Null(response);
        }
        #endregion
        #region CreateCandidate
        [Test]
        public void CreateCandidateSuccess()
        {
            CreateCandidateRequest req = fixture.Build<CreateCandidateRequest>()
                .With(c => c.id, fixture.Create<int>().ToString())
                .Create();
            var result = _controlelr.CreateCandidate(req);

            result.Should().BeOfType<JsonResult>().Which.Value.Should().BeOfType<CreateCandidateResponse>();

            var response = result.As<JsonResult>().Value.As<CreateCandidateResponse>();

            Assert.NotNull(response);
            Assert.AreEqual(req.id, response.id);
        }
        #endregion
        #region UpdateCandidate
        [Test]
        public void UpdateCandidate()
        {
            UpdateCandidateRequest req = fixture.Build<UpdateCandidateRequest>()
                .With(u => u.id, fixture.Create<int>().ToString())
                .Create();
            CandidateModel candidate = new CandidateModel(req);
            _dataAccessProvider.Setup(d => d.UpdateCandidate(It.IsAny<int>(),It.IsAny<CandidateModel>())).Returns(candidate);

            var result = _controlelr.UpdateCandidateById(It.IsAny<int>(), req);

            result.Should().BeOfType<JsonResult>().Which.Value.Should().BeOfType<UpdateCandidateResponse>();

            var response = result.As<JsonResult>().Value.As<UpdateCandidateResponse>();

            Assert.NotNull(response);
            Assert.AreEqual(req.id, response.id);
        }
        #endregion
        #region DeleteCandidate
        [Test]
        public void DeleteCandidateSuccess()
        {
            var result = _controlelr.DeleteCandidateById(It.IsAny<int>());

            result.Should().BeOfType<JsonResult>().Which.Value.Should().BeOfType<DeleteCandidateResponse>();

            var response = result.As<JsonResult>().Value.As<DeleteCandidateResponse>();

            Assert.NotNull(response);
            Assert.AreEqual("ok", response.status);
            Assert.Null(response.message);
        }
        [Test]
        public void DelteCandidateFailedReturnError()
        {
            _dataAccessProvider.Setup(d => d.DeleteCandidate(It.IsAny<int>())).Throws<Exception>();

            var result = _controlelr.DeleteCandidateById(It.IsAny<int>());

            result.Should().BeOfType<JsonResult>()
                .Which.Value.Should().BeOfType<DeleteCandidateResponse>()
                .Which.status.Should().Equals("error");

            var response = result.As<JsonResult>().Value.As<DeleteCandidateResponse>();

            Assert.NotNull(response);
            Assert.AreEqual("error", response.status);
            Assert.NotNull(response.message);
        }
        #endregion
    }
}
