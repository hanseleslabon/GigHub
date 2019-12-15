﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{

    public class Notification {

        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {

        }

        private Notification(NotificationType type, Gig gig)
        {
            if (type == null)
                throw new ArgumentNullException("Notification Type");

            Gig = gig ?? throw new ArgumentNullException("Gig");
            Type = Type ;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, String originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalVenue = originalVenue;
            notification.OriginalDateTime = originalDateTime;
            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}