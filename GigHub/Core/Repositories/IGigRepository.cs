using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        List<ApplicationUser> GetGigsUserFollowing(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetArtistUpcomingGig(string userId);
        IEnumerable<Gig> GetAllUpcomingGig();
    }
}