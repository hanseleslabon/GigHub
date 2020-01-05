using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowings(string followeeId, string followerId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FollowerID == followerId && f.FolloweeId == followeeId);
        }

        public IEnumerable<ApplicationUser> GetAllFollowee(string followerId)
        {
            return _context.Followings
                 .Where(f => f.FollowerID == followerId)
                 .Select(f => f.Followee)
                 .ToList();
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}