using AutoMapper;
using AutoMapper.QueryableExtensions;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfigurationProvider _configMapper;

        //TODO refactor autoMapper
        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
            _configMapper = new AutoMapperConfigContext().AutoMapperConfig;
        }

        public IEnumerable<Notification> GetNewNotification(string userId)
        {
            return _context.UserNotifications
               .Where(u => u.UserId == userId && !u.IsRead)
               .Select(u => u.Notification)
               .Include(g => g.Gig.Artist)
               .ToList();
        }

        public IEnumerable<NotificationDto> GetNewNotificationDto(string userId)
        {
            return _context.UserNotifications
               .Where(u => u.UserId == userId && !u.IsRead)
               .Select(u => u.Notification)
               .Include(g => g.Gig.Artist)
               .ProjectTo<NotificationDto>(_configMapper)
               .ToList();
        }
    }
}