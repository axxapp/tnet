using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using TCom.EF;
using TNet.Models.Statistic;

namespace TNet.BLL.Statistic {
    /// <summary>
    /// 投诉统计
    /// </summary>
    public class IssuesStatisticService {

        /// <summary>
        /// 根据一个日期向前推,取N天的投诉统计(按日统计)
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static List<IssuesStatisticByDateViewModel> StatisticByDayForwardDays(DateTime date, long days) {
            List<IssuesStatisticByDateViewModel> list = new List<IssuesStatisticByDateViewModel>();
            TN db = new TN();
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter() {
                     SqlValue=date,
                     ParameterName="@date"
                },new SqlParameter() {
                     SqlValue=days,
                     ParameterName="@days"
                }
            };
            string sql = "select CONVERT(varchar(100), cretime, 23) as [Date],count(*)  [Count] from [Issues] where DATEDIFF(dd,cretime,@date)<=@days and DATEDIFF(dd,cretime,@date)>=0 group by CONVERT(varchar(100), cretime, 23) order by CONVERT(varchar(100), cretime, 23) desc";
            list = db.Database.SqlQuery<IssuesStatisticByDateViewModel>(sql, paras).ToList();
            return list;

        }

        /// <summary>
        /// 根据一个日期对应的月向前推,取N个月的投诉统计(按月统计)
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public static List<IssuesStatisticByDateViewModel> StatisticByMonthForwardMonths(DateTime date, long months) {
            List<IssuesStatisticByDateViewModel> list = new List<IssuesStatisticByDateViewModel>();
            TN db = new TN();
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter() {
                     SqlValue=date,
                     ParameterName="@date"
                },new SqlParameter() {
                     SqlValue=months,
                     ParameterName="@months"
                }
            };
            string sql = "select (DateName(year,cretime)+'-'+DateName(month,cretime)) as [Date],count(*)  [Count] from [Issues] where DATEDIFF(MM,cretime,@date)<=@months and DATEDIFF(MM,cretime,@date)>=0 group by (DateName(year, cretime) + '-' + DateName(month, cretime)) order by (DateName(year, cretime) + '-' + DateName(month, cretime)) desc";
            list = db.Database.SqlQuery<IssuesStatisticByDateViewModel>(sql, paras).ToList();
            return list;

        }

        /// <summary>
        /// 根据一个日期对应的年向前推,取N年的投诉统计(按年统计)
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public static List<IssuesStatisticByDateViewModel> StatisticByYearForwardYears(DateTime date, long years) {
            List<IssuesStatisticByDateViewModel> list = new List<IssuesStatisticByDateViewModel>();
            TN db = new TN();
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter() {
                     SqlValue=date,
                     ParameterName="@date"
                },new SqlParameter() {
                     SqlValue=years,
                     ParameterName="@years"
                }
            };
            string sql = "select DateName(year,cretime) as [Date],count(*)  [Count] from [Issues] where DATEDIFF(MM,cretime,@date)<=@years and DATEDIFF(MM,cretime,@date)>=0 group by DateName(year, cretime) order by DateName(year, cretime) desc";
            list = db.Database.SqlQuery<IssuesStatisticByDateViewModel>(sql, paras).ToList();
            return list;

        }
    }
}