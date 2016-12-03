using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCom.Msg;
using TCom.Util;

namespace TComCoreService
{
    public sealed class Service
    {
        static int port = 6699;
        static TThread t;
        static TNetThread tnet;

        public static void init()
        {
            Service.stopt();
            t = new TThread();
            tnet = new TNetThread();
        }
        public static void stopt()
        {
            if (t != null)
            {
                t.Stop();
            }
            t = null;
            if (tnet != null)
            {
                tnet.Stop();
            }
            tnet = null;

        }

        private class TThread
        {
            public bool run;
            Thread t;
            Socket service;
            public TThread()
            {
                run = true;
                t = new Thread(startService);
                t.Start();
            }

            public void startService()
            {
                //得到本机IP，设置TCP端口号         
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, port);
                service = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //绑定网络地址
                service.Bind(ip);
                byte[] buffer = new byte[1024];
                while (run)
                {
                    try
                    {
                        EndPoint point = new IPEndPoint(IPAddress.Any, 0);
                        int length = service.ReceiveFrom(buffer, ref point);
                        //Pub.e("UDP=" + length);
                    }
                    catch (Exception)
                    {

                    }
                    if (run && tnet != null)
                    {
                        tnet.Set();
                    }
                }

            }



            public void Stop()
            {
                run = false;
                try
                {
                    service.Shutdown(SocketShutdown.Both);
                    service.Close();

                }
                catch (Exception)
                {
                }
                service = null;

            }
        }



        private class TNetThread
        {
            public bool run;
            Thread t;
            public ManualResetEvent _event = new ManualResetEvent(false);
            public object lockObj = new object();
            public TNetThread()
            {
                run = true;
                t = new Thread(startService);
                t.Start();
            }

            public void startService()
            {
                while (run)
                {
                    try
                    {
                        bool isPub = false;
                        using (TCom.EF.TN db = new TCom.EF.TN())
                        {
                        look:
                            try
                            {
                                isPub = false;
                                var mus = (from muo in db.ManageUsers
                                           join uo in db.Users on muo.idweixin equals uo.idweixin
                                           where (muo.idweixin != null && muo.idweixin.Trim() != string.Empty
                                           && muo.inuse == true && uo.inuse == true &&
                                           (muo.recv_order == true ||
                                           muo.recv_review == true ||
                                           muo.recv_setup == true ||
                                           muo.send_setup == true))
                                           select muo).ToList();
                                //Pub.e("Msg-Count:" + mus.Count);
                                List<TCom.EF.Msg> ms = db.Msgs.Where(m => m.inuse == true && m.status == 0).ToList();
                                int errcode = 0;
                                if (ms != null && mus != null)
                                {
                                    for (int i = 0; i < ms.Count; i++)
                                    {
                                        TCom.EF.Msg m = ms[i];

                                        if (m.type == MsgType.PostPayFinishOrder || m.type == MsgType.PostWaitReviewOrder)//订单支付完成，等待派发通知
                                        {
                                            isPub = true;
                                            m.status = 1;
                                            TCom.EF.MyOrder mo = db.MyOrders.Where(moo => moo.orderno == m.orderno).FirstOrDefault();
                                            if (mo != null)
                                            {
                                                TCom.EF.User uo = (from u in db.Users
                                                                   where (u.iduser == mo.iduser)
                                                                   select u).FirstOrDefault();
                                                if (uo != null)
                                                {
                                                    for (int j = 0; j < mus.Count; j++)
                                                    {
                                                        var mu = mus[j];
                                                        if (m.type == MsgType.PostPayFinishOrder && mu.recv_order)
                                                        {
                                                            MsgMgr.FinishPay(mo, uo, mu, db);
                                                        }
                                                        else if (m.type == MsgType.PostWaitReviewOrder && mu.recv_review)
                                                        {
                                                            MsgMgr.WaitReviewOrder(mo, uo, mu, db);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                        else if (m.type == MsgType.PayFinishOrder || m.type == MsgType.SetupOrder ||
                                            m.type == MsgType.PostWaitReviewOrder || m.type == MsgType.WaitReviewOrder ||
                                            m.type == MsgType.PauseTask || m.type == MsgType.FinishTask)
                                        {
                                            for (int g = 0; g < 2; g++)
                                            {
                                                JObject j = Msg.Send(m.msg1);
                                                errcode = j != null ? j.Value<int>("errcode") : -1;
                                                if (errcode == 0)
                                                {
                                                    m.status = 1;
                                                    break;
                                                }
                                                else if (errcode == 40001)
                                                {
                                                    isPub = true;
                                                    Pub.delAccessToken();
                                                }
                                            }

                                        }
                                        if (db.SaveChanges() > 0)
                                        {
                                            Pub.e("Msg-SaveChanges:" + 1);
                                        }
                                        else
                                        {
                                            isPub = false;
                                            Pub.e("Msg-SaveChanges:" + 0);
                                        }



                                    }
                                }

                            }
                            catch (Exception e)
                            {
                                Pub.e("error=" + e.InnerException + "_" + e.Message);
                                //throw e;
                            }
                            if (isPub)
                            {
                                goto look;
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Pub.e("error=" + e.InnerException + "_+_+_+_+_+" + e.Message);
                        //throw e;
                    }
                    _event.Reset();
                    Pub.e("WaitOne=" + DateTime.Now);
                    _event.WaitOne(1000 * 60 * 10);
                    Pub.e("WaitOne-f=" + DateTime.Now);
                }
                _event = null;
            }

            public void Set()
            {
                _event.Set();
            }

            public void Stop()
            {
                run = false;
                Set();
            }
        }
    }
}
