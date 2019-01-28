﻿using System.Collections.Generic;

namespace DnnSummit.Data.Models
{
    public class ScheduleDetails : Entity
    {
        public string Title { get; set; }
        public string CardDescription { get; set; }
        public string BannerTitle { get; set; }
        public string BannerHeading { get; set; }
        public byte[] BannerImage { get; set; }
        public IEnumerable<Content> Sections { get; set; }
    }
}
