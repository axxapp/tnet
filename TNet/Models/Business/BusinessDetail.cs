using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Business
{
    public class BusinessDetail
    {
        public TCom.EF.Business Buss { get; set; }

        public List<string> Imgs { get; set; }
    }
}