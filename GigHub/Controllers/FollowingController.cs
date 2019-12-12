using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace GigHub.Controllers
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

            var result = _context.Followings.Any
                (f => f.FolloweeId == dto.FolloweeId && f.FollowerID == userId);

            if (result)
            {
                return BadRequest("You are already following this artist");
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


    }
}
