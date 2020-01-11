using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendaceControllerTest
    {
        private string _userId = "1";
        private string _userName = "user1@domain.com";

        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _controller.MockCurrentUser(_userId, _userName);
        }

        [TestMethod]
        public void Attend_UserAttendingAGigForWhichHeHasAnAttendance_ShouldReturnBadRequest()
        {
            var attendance = new Attendance();

            _mockRepository.Setup(r => r.GetAttendances(1, _userId)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttendance_NoAttendanceWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.DeleteAttendance(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnOk()
        {
            var attedance = new Attendance();
            _mockRepository.Setup(x => x.GetAttendances(1, _userId)).Returns(attedance);

            var result = _controller.DeleteAttendance(1);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }
        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnTheIdOfDeletedAttendance()
        {
            var attedance = new Attendance();
            _mockRepository.Setup(x => x.GetAttendances(1, _userId)).Returns(attedance);

            var result = (OkNegotiatedContentResult<int>) _controller.DeleteAttendance(1);

            result.Content.Should().Be(1);
        }
    }

}