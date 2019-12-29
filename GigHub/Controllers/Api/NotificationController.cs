using AutoMapper;
using AutoMapper.QueryableExtensions;
using GigHub.App_Start;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationController : ApiController
    {
        private ApplicationDbContext _context;
        private IConfigurationProvider _configMapper;

        public NotificationController()
        {
            _context = new ApplicationDbContext();
            _configMapper = new AutoMapperConfigContext().AutoMapperConfig;
        }

        public IEnumerable<NotificationDto> GetNewNotification()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(u => u.UserId == userId && !u.IsRead)
                .Select(u => u.Notification)
                .Include(g => g.Gig.Artist)
                .ProjectTo<NotificationDto>(_configMapper)
                .ToList();

            return notifications;
        }   

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications.Where(n => n.UserId == userId && !n.IsRead).ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
