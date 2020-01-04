﻿using GigHub.Models;
using GigHub.Repository;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _gigRepository = new GigRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var upComingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upComingGigs = upComingGigs.Where(g => g.Artist.Name.Contains(query) ||
                  g.Venue.Contains(query) || g.Genre.Name.Contains(query));
            }

            var userId = User.Identity.GetUserId();

            var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId);

            var followings = _context.Followings
                .Where (a => a.FollowerID == userId)
                .Select(f => f.Followee)
                .ToList()
                .ToLookup(a => a.Id);

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = upComingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances,
                Followings = followings,
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}