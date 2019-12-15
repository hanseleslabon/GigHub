﻿using GigHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Gig
    {
        public int  Id { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public byte GenreId { get; set; } 
        public Genre Genre { get; set; }

        public bool IsCanceled { get; private set; }
        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        internal void Canceled()
        {
            IsCanceled = true;

            var notification =  Notification.GigCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        internal void Modify(DateTime dateTime, string venue, byte Genre)
        {
            var notification = Notification.GigUpdated(this, dateTime, venue);


            Venue = venue;
            DateTime = dateTime;
            GenreId = Genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }

    public class Genre
    {
        public byte Id  { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }
    }
}