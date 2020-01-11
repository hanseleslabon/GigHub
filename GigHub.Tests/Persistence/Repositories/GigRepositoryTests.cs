using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHub.Tests.Persistence.Repositories
{
    public static class MockDbSetExtensions
    {
        public static void SetSource<T>(this Mock<DbSet<T>> mockSet, IList<T> source) where T : class
        {
            var data = source.AsQueryable();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }


    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendances;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();
            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetArtistUpcomingGig("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Canceled();

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetArtistUpcomingGig("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "2" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetArtistUpcomingGig("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetArtistUpcomingGig("1");

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-2) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(12) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending("2");

            gigs.Should().BeEmpty();

        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsInThFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(12) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().Contain(gig);
        }
    }
}
