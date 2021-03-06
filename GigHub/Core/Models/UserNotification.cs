﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class UserNotification {

        [Key]
        [Column(Order=1)]
        public string UserId { get;  set; }

        public ApplicationUser User { get; private set; }


        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public Notification Notification { get;  set; }

        public bool IsRead { get;  set; }

        public UserNotification()
        {

        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            Notification = notification ?? throw new ArgumentNullException("Notification");
            User = user ?? throw new ArgumentNullException("User");

            UserId = user.Id;
            NotificationId = Notification.Id;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}