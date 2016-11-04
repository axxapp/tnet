using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using TCom.EF;
using TNet.Models;

namespace TNet.BLL {
    public class OrderStatisticService {
        /// <summary>
        /// 根据一个日期向前推,取N天的订单统计(按日统计)
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static List<OrderStatisticByDateViewModel> StatisticByDayForwardDays(DateTime date,long days) {
            List<OrderStatisticByDateViewModel> list = new List<OrderStatisticByDateViewModel>();
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
            string sql = "select CONVERT(varchar(100), cretime, 23) as [Date],sum(totalfee) as OrderAmount,count(*)  OrderNumer from MyOrder where DATEDIFF(dd,cretime,@date)<=@days and DATEDIFF(dd,cretime,@date)>=0 group by CONVERT(varchar(100), cretime, 23)";
            list=db.Database.SqlQuery<OrderStatisticByDateViewModel>(sql,paras).ToList();
            return list;

        }

        /// <summary>
        /// 根据一个日期对应的月向前推,取N个月的订单统计(按月统计)
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public static List<OrderStatisticByDateViewModel> StatisticByMonthForwardMonths(DateTime date, long months) {
            List<OrderStatisticByDateViewModel> list = new List<OrderStatisticByDateViewModel>();
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
            string sql = "select (DateName(year,cretime)+'-'+DateName(month,cretime)) as [Date],sum(totalfee) as OrderAmount,count(*)  OrderNumer from MyOrder where DATEDIFF(MM,cretime,@date)<=@months and DATEDIFF(MM,cretime,@date)>=0 group by (DateName(year, cretime) + '-' + DateName(month, cretime))";
            list = db.Database.SqlQuery<OrderStatisticByDateViewModel>(sql, paras).ToList();
            return list;

        }

        /// <summary>
        /// 根据一个日期对应的年向前推,取N年的订单统计(按年统计)
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public static List<OrderStatisticByDateViewModel> StatisticByYearForwardYears(DateTime date, long years) {
            List<OrderStatisticByDateViewModel> list = new List<OrderStatisticByDateViewModel>();
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
            string sql = "select DateName(year,cretime) as [Date],sum(totalfee) as OrderAmount,count(*)  OrderNumer from MyOrder where DATEDIFF(MM,cretime,@date)<=@years and DATEDIFF(MM,cretime,@date)>=0 group by DateName(year, cretime)";
            list = db.Database.SqlQuery<OrderStatisticByDateViewModel>(sql, paras).ToList();
            return list;

        }
    }
}