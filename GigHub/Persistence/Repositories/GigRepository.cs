﻿using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
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

        public IEnumerable<Gig> GetArtistUpcomingGig(string userId)
        {
            return _context.Gigs
                    .Where(g => g.ArtistId == userId
                             && g.DateTime > DateTime.Now
                             && !g.IsCanceled)
                             .Include(g => g.Genre)
                             .ToList();
        }

        public IEnumerable<Gig> GetAllUpcomingGig()
        {
            return _context.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime> DateTime.Now)
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

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}