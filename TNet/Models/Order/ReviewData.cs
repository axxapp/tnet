using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Order
{
    public class ReviewData
    {
        public string mgcode { get; set; }

        public string iduser { get; set; }

        public string orderno { get; set; }


        public string content { get; set; }


        public bool review { get; set; }
    }
}