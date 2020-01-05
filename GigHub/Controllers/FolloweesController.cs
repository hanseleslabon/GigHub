using GigHub.Core;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View("Following", _unitOfWork.Following.GetAllFollowee(User.Identity.GetUserId()));
        }

    }
}
