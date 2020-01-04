using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Repository
{
    public class GigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                  .Include(g => g.Attendances.Select(a => a.Attendee))
                  .SingleOrDefault(s => s.Id == gigId);
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                 .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetMyUpcomingGig(string userId)
        {
            return _context.Gigs
                    .Where(g => g.ArtistId == userId
                             && g.DateTime > DateTime.Now
                             && !g.IsCanceled)
                             .Include(g => g.Genre)
                             .ToList();
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public List<ApplicationUser> GetGigsUserFollowing(string userId)
        {
            return _context.Followings.Where
                                        (a => a.FollowerID == userId)
                                        .Select(f => f.Followee)
                                        .ToList();
        }
    }
}