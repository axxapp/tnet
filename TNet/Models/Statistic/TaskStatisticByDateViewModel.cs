using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Statistic
{
    /// <summary>
    /// 统计工单实体
    /// </summary>
    public class TaskStatisticByDateViewModel : StatisticBaseViewModel
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 工单类型
        /// </summary>
        public int TaskType { get; set; }
    }
}