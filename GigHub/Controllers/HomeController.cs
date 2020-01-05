using GigHub.Core;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upComingGigs = _unitOfWork.Gigs.GetAllUpcomingGig();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upComingGigs = upComingGigs.Where(g => g.Artist.Name.Contains(query) ||
                  g.Venue.Contains(query) || g.Genre.Name.Contains(query));
            }

            var userId = User.Identity.GetUserId();

            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId);

            var followings = _unitOfWork.Following.GetAllFollowee(userId).ToLookup(a => a.Id);

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