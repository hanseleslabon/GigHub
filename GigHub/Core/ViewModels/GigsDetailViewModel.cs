﻿using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.ViewModels
{
    public class GigsDetailViewModel
    {
        public Gig Gig { get; set; } 

        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}