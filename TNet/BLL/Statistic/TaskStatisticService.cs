using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCom.EF;
using System.Data;
using System.Data.SqlClient;
using TNet.Models.Statistic;

namespace TNet.BLL.Statistic
{
    /// <summary>
    /// 工单统计
    /// </summary>
    public class TaskStatisticService
    {

        /// <summary>
        /// 按工单类型统计
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public static List<TaskStatisticByDateViewModel> StatisticByType(DateTime sdate, DateTime edate)
        {
            List<TaskStatisticByDateViewModel> list = new List<TaskStatisticByDateViewModel>();
            TN db = new TN();
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter() {
                     SqlValue=sdate,
                     ParameterName="@sdate"
                },new SqlParameter() {
                     SqlValue=edate,
                     ParameterName="@edate"
                }
            };
            string sql = "select CONVERT(varchar(100), @sdate, 23)+'-'+CONVERT(varchar(100), @edate, 23) as [Date],count(*)  [Count],[tasktype] as TaskType from [Task] where DATEDIFF(dd, [cretime], @sdate)<= 0 and DATEDIFF(dd, [cretime], @edate) >= 0 group by [tasktype] order by [tasktype]";
            list = db.Database.SqlQuery<TaskStatisticByDateViewModel>(sql, paras).ToList();
            return list;

        }
    }
}