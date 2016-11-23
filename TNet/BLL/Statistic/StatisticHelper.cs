using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TNet.Models.Statistic;

namespace TNet.BLL.Statistic
{
    /// <summary>
    /// 统计帮助类
    /// </summary>
    public class StatisticHelper<T> where T : StatisticBaseViewModel, new()
    {
        public static void CalculateDays(DateTime date, long days, List<T> list)
        {
            CalculateDate(date,days,list,"yyyy-MM-dd");
        }

        public static void CalculateMonths(DateTime date, long months, List<T> list)
        {
            CalculateDate(date, months, list, "yyyy-MM");
        }

        public static void CalculateYears(DateTime date, long years, List<T> list)
        {
            CalculateDate(date, years, list, "yyyy");
        }

        private static void CalculateDate(DateTime date, long adds, List<T> list,string dateFormate) 
        {
            if (list == null)
            {
                list=new List<T>();
            }
            List<string> dates = new List<string>();
            for (int i = 0; i < adds; i++)
            {
                string dateStr =string.Empty;
                if (dateFormate== "yyyy-MM-dd") {
                    dateStr = date.AddDays(-i).ToString(dateFormate);
                }
                else if (dateFormate == "yyyy-MM")
                {
                    dateStr = date.AddMonths(-i).ToString(dateFormate);
                }
                else if (dateFormate == "yyyy")
                {
                    dateStr = date.AddYears(-i).ToString(dateFormate);
                }
                List<T> temps = list.Where(en => en.Date == dateStr).ToList();
                if (temps == null || temps.Count == 0)
                {
                    T t = new T();
                    t.Date = dateStr;
                    list.Add(t);
                }
            }
            if (dateFormate == "yyyy-MM-dd")
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Date= Convert.ToDateTime(list[i].Date).ToString("MM-dd");
                }
            }
        }

    }
}