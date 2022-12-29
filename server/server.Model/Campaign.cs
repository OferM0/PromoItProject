﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class Campaign
    {
        public int Id { get; set; }
        public string OrganizationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Hashtag { get; set; }
        public bool Active { get; set; }
    }
}