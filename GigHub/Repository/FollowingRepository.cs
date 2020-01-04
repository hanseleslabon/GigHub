using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repository
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowings(string followeeId , string followerId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FollowerID == followerId && f.FolloweeId == followeeId);
        }
    }
}