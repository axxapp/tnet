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
        /// 根据一个日期向前推取N天的订单统计(按日统计)
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
            string sql = "select CONVERT(varchar(100), cretime, 23) as [Date],sum(totalfee) as OrderAmount,count(*)  OrderNumer from MyOrder where DATEDIFF(dd,cretime,@date)<=10 and DATEDIFF(dd,cretime,@date)>=0 group by CONVERT(varchar(100), cretime, 23)";
            list=db.Database.SqlQuery<OrderStatisticByDateViewModel>(sql,paras).ToList();
            return list;

        }
    }
}