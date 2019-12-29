using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingController : ApiController
    {
        ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            var result = _context.Followings.SingleOrDefault
                (f => f.FolloweeId == dto.FolloweeId && f.FollowerID == userId);

            if (result != null)
            {
                return NotFound();
            }

            var follow = new Following()
            {
                FolloweeId = dto.FolloweeId,
                FollowerID = userId
            };

            _context.Followings.Add(follow);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteFollowing(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _context.Followings.SingleOrDefault(a => a.FolloweeId  == id && a.FollowerID == userId);

            if (following == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(following);
            _context.SaveChanges();

            return Ok(id);
        }


    }
}
