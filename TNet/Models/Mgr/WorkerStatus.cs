using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Mgr
{
    public class WorkerStatus
    {
        /// <summary>
        /// 已接单
        /// </summary>
        public static readonly int Revice = 100;


        /// <summary>
        /// 工作中
        /// </summary>
        public static readonly int Doing = 200;

        /// <summary>
        /// 暂结
        /// </summary>
        public static readonly int Pause = 300;

        /// <summary>
        /// 完工
        /// </summary>
        public static readonly int Finish = 400;


        /// <summary>
        /// 申请取消
        /// </summary>
        public static readonly int Canceling = 500;



        /// <summary>
        /// 取消成功
        /// </summary>
        public static readonly int Cancel = 600;

        /// <summary>
        /// 申请转移
        /// </summary>
        public static readonly int Transfering = 700;

        /// <summary>
        /// 转移成功
        /// </summary>
        public static readonly int Transfer = 800;



    }
}