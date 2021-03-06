﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TNet.Models.Order;
using TNet.Models.Statistic;

namespace TNet.Models {
    /// <summary>
    /// 订单按日期(日/月/年)统计实体
    /// </summary>
    public class OrderStatisticByDateViewModel:StatisticBaseViewModel {
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNumer { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public double OrderAmount { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public int OrderType { get; set; } 
    }
}