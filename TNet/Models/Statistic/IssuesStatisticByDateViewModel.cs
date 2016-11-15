using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Statistic {
    /// <summary>
    /// 统计投诉实体
    /// </summary>
    public class IssuesStatisticByDateViewModel:StatisticBaseViewModel {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}