using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCom.Com;


namespace TCom.Msg
{
    //发送消息
    public sealed class MsgMgr
    {

        /// <summary>
        /// 发送支付订单消息
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="otype"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool PostFinishPay(string orderno, int otype, TCom.EF.TN db)
        {
            return crateMsg("", "", MsgType.PostPayFinishOrder, orderno, otype, db);

        }


        /// <summary>
        /// 支付完成
        /// </summary>
        /// <param name="idweixin"></param>
        /// <param name="mo"></param>
        /// <param name="iduser"></param>
        /// <param name="uname"></param>
        /// <param name="db"></param>
        /// <returns></returns>

        public static bool FinishPay(string idweixin, EF.MyOrder mo, string iduser, string uname, TCom.EF.TN db)
        {
            int otype = mo.otype != null ? mo.otype.Value : 0;
            JObject jo = new JObject();
            jo["touser"] = idweixin;
            jo["template_id"] = "_5rsT-d9H1iLDHr8B7IN5IYo4QftrnNxNofEeTn4EyI";

            jo["url"] = "http://app.i5shang.com/tnet/order/detail/" + mo.orderno + "?iduser=" + iduser
                + "&updateUser=1";
            JObject jdo = new JObject();
            jdo["first"] = getJobj(mo.merc + "(" + mo.spec + ")");
            jdo["tradeDateTime"] = getJobj(mo.cretime != null ? mo.cretime.Value.ToString("MM月dd日 HH时mm分") : "无");
            jdo["orderType"] = getJobj((otype == 1) ? "宽带" : "报装");
            jdo["customerInfo"] = getJobj(uname);
            jdo["orderItemName"] = getJobj("交易金额");
            jdo["orderItemData"] = getJobj(mo.totalfee + "");
            jdo["remark"] = getJobj("欢迎再次购买");
            jo["data"] = jdo;

            return crateMsg(idweixin, jo.ToString(), MsgType.PayFinishOrder, mo.orderno + "", otype, db);

        }



        /// <summary>
        /// 派单
        /// </summary>
        /// <param name="idweixin"></param>
        /// <param name="mo"></param>
        /// <param name="iduser"></param>
        /// <param name="uname"></param>
        /// <param name="db"></param>
        /// <returns></returns>

        public static bool SetupOrder(EF.MyOrder mo, EF.User user, TCom.EF.TN db)
        {
            int otype = mo.otype != null ? mo.otype.Value : 0;
            JObject jo = new JObject();
            jo["touser"] = user.idweixin;
            jo["template_id"] = "GeHNTXa7V_S5Q4uGaFYq1vzXmZZTAfy8wKJyT4muV28";
            jo["url"] = "http://app.i5shang.com/tnet/order/detail/" + mo.orderno + "?iduser=" + user.iduser
                + "&updateUser=1";
            JObject jdo = new JObject();
            jdo["first"] = getJobj(mo.merc + "(" + mo.spec + ")");
            jdo["keyword1"] = getJobj("报装");
            jdo["keyword2"] = getJobj(mo.orderno + "");
            jdo["keyword3"] = getJobj(user.name);
            jdo["keyword4"] = getJobj(user.phone);
            jdo["keyword5"] = getJobj("已线上缴费,现场无需收费.");
            jdo["remark"] = getJobj("用户希望服务人员到达" + mo.addr + "进行服务，点击查看详情...");
            jo["data"] = jdo;
            return crateMsg(user.idweixin, jo.ToString(), MsgType.SetupOrder, mo.orderno + "", otype, db);

        }


        public static bool PostReviewOrder(string orderno, int otype, TCom.EF.TN db)
        {
            return crateMsg("", "", MsgType.PostWaitReviewOrder, orderno, otype, db);

        }




        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="idweixin"></param>
        /// <param name="mo"></param>
        /// <param name="iduser"></param>
        /// <param name="uname"></param>
        /// <param name="db"></param>
        /// <returns></returns>

        public static bool WaitReviewOrder(EF.MyOrder mo, EF.User user, TCom.EF.TN db)
        {
            int otype = mo.otype != null ? mo.otype.Value : 0;
            JObject jo = new JObject();
            jo["touser"] = user.idweixin;
            jo["template_id"] = "i72ctUGg8ILHaZaYJrp1NvCYZFZoRicS7sC_Ozzmhqs";
            jo["url"] = "http://app.i5shang.com/tnet/order/detail/" + mo.orderno + "?iduser=" + user.iduser
                + "&updateUser=1";
            JObject jdo = new JObject();
            jdo["first"] = getJobj("请审核以下订单");
            jdo["keyword1"] = getJobj(mo.orderno + "");
            jdo["keyword2"] = getJobj(mo.totalfee + "");
            jdo["keyword3"] = getJobj(mo.cretime.Value.ToString("yyyy-MM-dd"));
            jdo["keyword4"] = getJobj("用户 [" + user.name + "]");
            jdo["remark"] = getJobj("请尽快审核！");
            jo["data"] = jdo;
            return crateMsg(user.idweixin, jo.ToString(), MsgType.WaitReviewOrder, mo.orderno + "", otype, db);

        }



        public static bool crateMsg(string idweixin, string msg, int msgType, string orderno, int otype, TCom.EF.TN db)
        {
            TCom.EF.Msg mo = new TCom.EF.Msg();
            mo.idweixin = idweixin;
            mo.idmsg = P.ID().ToString();
            mo.msg1 = msg;
            mo.cretime = DateTime.Now;
            mo.status = 0;
            mo.orderno = orderno;
            mo.otype = otype;
            mo.type = msgType;
            mo.inuse = true;
            if (db == null)
            {
                using (db = new TCom.EF.TN())
                {
                    db.Msgs.Add(mo);
                    if (db.SaveChanges() > 0)
                    {
                        Post();
                        return true;
                    }
                }
            }
            else
            {
                db.Msgs.Add(mo);
            }
            return true;
        }




        public static JObject getJobj(string value, string color = null)
        {
            JObject jio = new JObject();
            jio["value"] = value;
            if (!string.IsNullOrWhiteSpace(color))
            {
                jio["color"] = color;
            }
            return jio;
        }



        public static bool Post()
        {
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint host = new IPEndPoint(ip, 6699);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //server.SetSocketOption()
                byte[] data = new byte[] { 1 };
                int c = server.SendTo(data, data.Length, SocketFlags.None, host);
                if (c >= 1)
                {
                    return true;
                }
            }
            catch (Exception)
            {

            }

            return false;

        }
    }
}
