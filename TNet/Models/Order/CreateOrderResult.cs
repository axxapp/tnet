﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Order
{
    public class CreateOrderResult
    {
        public string url { get; set; }

        public long orderno { get; set; }

        public string order { get; set; }
    }
}