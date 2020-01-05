using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public FollowingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            var result = _unitOfWork.Following.GetFollowings(dto.FolloweeId, userId);
                
            if (result != null)
            {
                return NotFound();
            }

            var follow = new Following()
            {
                FolloweeId = dto.FolloweeId,
                FollowerID = userId
            };

            _unitOfWork.Following.Add(follow);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteFollowing(string id)
        {
            var following = _unitOfWork.Following.GetFollowings(id, User.Identity.GetUserId());

            if (following == null)
            {
                return NotFound();
            }

            _unitOfWork.Following.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }


    }
}
