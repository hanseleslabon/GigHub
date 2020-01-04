﻿using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Repository
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances.Where
                            (a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).ToList();
        }

        public Attendance GetAttendances(int gigId, string userId)
        {
            return _context.Attendances.SingleOrDefault
                            (a => a.GigId == gigId 
                            && a.AttendeeId == userId);
        }



    }
}