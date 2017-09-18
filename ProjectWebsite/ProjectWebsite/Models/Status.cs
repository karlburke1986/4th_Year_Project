using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebsite.Models
{
    public class Status
    {
        public int Open { get; set; }
        public int InProgress { get; set; }
        public int Closed { get; set; }
        public int Suspended { get; set; }
        public int Referred { get; set; }
        public int Pending { get; set; }
    }
}