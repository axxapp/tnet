﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCom.Msg
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public sealed class MsgType
    {

        /// <summary>
        /// 投递支付订单
        /// </summary>
        public readonly static int PostCreateOrder = 10;


        /// <summary>
        /// 投递支付订单
        /// </summary>
        public readonly static int PostPayFinishOrder = 20;


        /// <summary>
        /// 创建订单
        /// </summary>
        public readonly static int CreateOrder = 100;


        /// <summary>
        /// 投递等待审核订单
        /// </summary>
        public readonly static int PostWaitReviewOrder = 110;



        /// <summary>
        /// 等待审核订单
        /// </summary>
        public readonly static int WaitReviewOrder = 120;


        /// <summary>
        /// 支付订单
        /// </summary>
        public readonly static int PayFinishOrder = 200;

        /// <summary>
        /// 报装订单
        /// </summary>
        public readonly static int SetupOrder = 300;

        /// <summary>
        /// 暂结工单
        /// </summary>
        public readonly static int PauseTask = 400;


        /// <summary>
        /// 完工工单
        /// </summary>
        public readonly static int FinishTask = 500;


        /// <summary>
        /// 快过期订单
        /// </summary>
        public readonly static int ExpirePreOrder = 600;

        /// <summary>
        /// 过期订单
        /// </summary>
        public readonly static int ExpireOrder = 700;



        /// <summary>
        /// 投递问题
        /// </summary>
        public readonly static int PostCreateIssue = 800;


        /// <summary>
        /// 问题
        /// </summary>
        public readonly static int Issue = 900;
    }
}
