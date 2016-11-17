using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Statistic
{
    /// <summary>
    /// 统计用户实体
    /// </summary>
    public class UserStatisticByDateViewModel : StatisticBaseViewModel
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}