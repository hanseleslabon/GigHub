using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        IEnumerable<ApplicationUser> GetAllFollowee(string followerId);
        Following GetFollowings(string followeeId, string followerId);
        void Add(Following follow);
        void Remove(Following follow);
    }
}