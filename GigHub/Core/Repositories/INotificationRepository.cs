using System.Collections.Generic;
using GigHub.Core.Dto;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotification(string userId);
        IEnumerable<NotificationDto> GetNewNotificationDto(string userId);
    }
}