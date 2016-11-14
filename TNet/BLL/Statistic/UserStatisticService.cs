using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using TNet.Models.Statistic;
using TCom.EF;

namespace TNet.BLL.Statistic
{
    /// <summary>
    /// 用户统计
    /// </summary>
    public class UserStatisticService
    {
        /// <summary>
        /// 根据一个日期向前推,取N天的用户注册统计(按日统计)
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static List<UserStatisticByDateViewModel> StatisticByDayForwardDays(DateTime date, long days)
        {
            List<UserStatisticByDateViewModel> list = new List<UserStatisticByDateViewModel>();
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
            string sql = "select CONVERT(varchar(100), cretime, 23) as [Date],count(*)  [Count] from [User] where DATEDIFF(dd,cretime,@date)<=@days and DATEDIFF(dd,cretime,@date)>=0 group by CONVERT(varchar(100), cretime, 23) order by CONVERT(varchar(100), cretime, 23) desc";
            list = db.Database.SqlQuery<UserStatisticByDateViewModel>(sql, paras).ToList();
            return list;

        }

        /// <summary>
        /// 根据一个日期对应的月向前推,取N个月的用户注册统计(按月统计)
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public static List<UserStatisticByDateViewModel> StatisticByMonthForwardMonths(DateTime date, long months)
        {
            List<UserStatisticByDateViewModel> list = new List<UserStatisticByDateViewModel>();
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
            string sql = "select (DateName(year,cretime)+'-'+DateName(month,cretime)) as [Date],count(*)  [Count] from [User] where DATEDIFF(MM,cretime,@date)<=@months and DATEDIFF(MM,cretime,@date)>=0 group by (DateName(year, cretime) + '-' + DateName(month, cretime)) order by (DateName(year, cretime) + '-' + DateName(month, cretime)) desc";
            list = db.Database.SqlQuery<UserStatisticByDateViewModel>(sql, paras).ToList();
            return list;

        }

        /// <summary>
        /// 根据一个日期对应的年向前推,取N年的用户注册统计(按年统计)
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public static List<UserStatisticByDateViewModel> StatisticByYearForwardYears(DateTime date, long years)
        {
            List<UserStatisticByDateViewModel> list = new List<UserStatisticByDateViewModel>();
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
            string sql = "select DateName(year,cretime) as [Date],count(*)  [Count] from [User] where DATEDIFF(MM,cretime,@date)<=@years and DATEDIFF(MM,cretime,@date)>=0 group by DateName(year, cretime) order by DateName(year, cretime) desc";
            list = db.Database.SqlQuery<UserStatisticByDateViewModel>(sql, paras).ToList();
            return list;

        }

    }
}