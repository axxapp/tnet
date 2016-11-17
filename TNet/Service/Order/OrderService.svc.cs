using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using TCom.EF;
using TCom.Msg;
using TCom.Util;
using TNet.Models.Order;
using TNet.Models.Service.Com;
using TNet.Util;

namespace TNet.Service.Order
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“OrderService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 OrderService.svc 或 OrderService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class OrderService : IOrderService
    {
        public Result<CreateOrderResult> Create(CreateOrderData data)
        {
            Result<CreateOrderResult> result = new Result<CreateOrderResult>();
            result.Msg = "下单失败";
            if (data != null && !string.IsNullOrWhiteSpace(data.idmerc) && !string.IsNullOrWhiteSpace(data.idspec) &&
                !string.IsNullOrWhiteSpace(data.iduser) && data.count > 0)
            {
                try
                {
                    using (TN db = new TN())
                    {
                        var uo = db.Users.Where(m => m.iduser == data.iduser && m.inuse == true).FirstOrDefault();
                        if (uo == null)
                        {
                            result.Msg = "不存在改用户";
                            return result;
                        }
                        //string uname = db.Users.Where(m => m.iduser == data.iduser && m.inuse == true).Select(m => m.name).FirstOrDefault();
                        var mo = db.Specs.Where(m => m.idmerc == data.idmerc && m.idspec == data.idspec && m.inuse == true).FirstOrDefault();
                        if (mo == null)
                        {
                            result.Msg = "商品不存在 或已经下架";
                            return result;
                        }
                        MyOrder o = data.getData();
                        if (o != null)
                        {
                            o.price = mo.price;
                            o.totalfee = o.price * o.count;
                            db.MyOrders.Add(o);
                            MyOrderPress s = getMyOrderPress(o.orderno.ToString(), OrderStatus.Create, "用户 [" + data.uname + "]");
                            db.MyOrderPresses.Add(s);
                            if (o.status == OrderStatus.WaitPay)
                            {
                                s = getMyOrderPress(o.orderno.ToString(), OrderStatus.Confirm, "系统");
                                db.MyOrderPresses.Add(s);
                            }
                            else if (o.status == OrderStatus.WaitReview)
                            {
                                MsgMgr.PostReviewOrder(o.orderno + "", o.otype.Value, db);
                            }
                            if (db.SaveChanges() > 0)
                            {
                                result.Data = new CreateOrderResult();
                                if (o.status == OrderStatus.WaitPay)
                                {
                                    result.Data.url = "Order/Pay?orderno=" + o.orderno;
                                }
                                else
                                {
                                    result.Data.url = "Order/List";
                                }
                                result.Data.orderno = o.orderno;
                                result.Code = R.Ok;
                                MsgMgr.Post();
                            }
                            else
                            {
                                result.Code = R.Error;
                            }
                        }
                        // result.Data = m;
                    }
                }
                catch (Exception)
                {
                    result.Code = R.Error;
                    result.Msg = "出现异常";
                }
            }
            else
            {
                result.Code = R.Error;
                result.Msg = "商品、用户编号有误";
            }
            return result;
        }
        public Result<bool> Update(UpdateData data)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                using (TCom.EF.TN db = new TCom.EF.TN())
                {
                    TCom.EF.User uo = db.Users.Where(m => m.iduser == data.iduser && m.inuse == true).FirstOrDefault();
                    TCom.EF.MyOrder o = db.MyOrders.Where(m => m.orderno == data.orderno && m.iduser == data.iduser && m.inuse == true && m.status == OrderStatus.ReviewFail).FirstOrDefault();
                    if (o != null && uo != null)
                    {
                        db.MyOrders.Attach(o);
                        o.contact = data.contact;
                        o.addr = data.addr;
                        o.phone = data.phone;
                        o.notes = data.notes;
                        o.idc = data.idc;
                        o.idc_img1 = data.idc_img1;
                        o.idc_img2 = data.idc_img2;
                        o.idc_img3 = data.idc_img3;
                        o.status = OrderStatus.WaitReview;
                        MyOrderPress s = getMyOrderPress(o.orderno.ToString(), OrderStatus.UserUpdate, "用户 [" + uo.name + "]");
                        db.MyOrderPresses.Add(s);
                        if (o.status == OrderStatus.WaitReview)
                        {
                            MsgMgr.PostReviewOrder(o.orderno + "", o.otype.Value, db);
                        }
                        if (db.SaveChanges() > 0)
                        {
                            result.Data = true;
                            result.Code = R.Ok;
                            MsgMgr.Post();
                        }
                        else
                        {
                            result.Msg = "更新失败";
                            result.Code = R.Error;
                        }
                    }
                    else
                    {
                        result.Msg = "订单不存在 或 已经发生了改变";
                    }
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
                result.Msg = "出现异常";
            }
            return result;

        }
        private MyOrderPress getMyOrderPress(string orderno, int status, string oper, string notes = "")
        {
            MyOrderPress s = new MyOrderPress();
            s.idpress = Pub.ID();
            s.orderno = orderno;
            s.status = status;
            s.statust = OrderStatus.get(s.status).text;
            s.oper = oper;
            s.notes = notes;
            s.inuse = true;
            s.cretime = DateTime.Now;
            return s;
        }

        public Result<OrderListInfo> List(string iduser)
        {
            Result<OrderListInfo> result = new Result<OrderListInfo>();
            try
            {
                if (!string.IsNullOrWhiteSpace(iduser))
                {
                    using (TN db = new TN())
                    {
                        result.Data = new OrderListInfo()
                        {
                            Order = db.MyOrders.Where(m => m.inuse == true && m.iduser == iduser).OrderByDescending(m => m.cretime).ToList()
                        };
                        result.Code = R.Ok;
                    }
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
                result.Msg = "出现异常";
            }
            return result;
        }


        public Result<bool> Cancel(string iduser, string orderno)
        {
            Result<bool> result = new Result<bool>();
            result.Code = R.Error;
            result.Msg = "取消订单失败";
            try
            {
                if (!string.IsNullOrWhiteSpace(iduser) && !string.IsNullOrWhiteSpace(orderno))
                {
                    long _iduser = long.Parse(iduser);
                    long _orderno = long.Parse(orderno);
                    using (TN db = new TN())
                    {
                        using (DbContextTransaction t = db.Database.BeginTransaction())
                        {

                            int r = db.Database.ExecuteSqlCommand("update myorder set status = {0} where iduser = {1} and orderno = {2}", OrderStatus.Cancel, _iduser, _orderno);

                            if (r > 0)
                            {
                                r = db.Database.ExecuteSqlCommand("insert into MyOrderPress (idpress,orderno,status,statust,cretime,oper,inuse) values({0},{1},{2},{3},{4},{5},1)", Pub.ID(), orderno, OrderStatus.Cancel, OrderStatus.get(OrderStatus.Cancel).text, DateTime.Now, "用户");
                                if (r > 0)
                                {
                                    t.Commit();
                                    result.Data = true;
                                    result.Code = R.Ok;
                                    return result;
                                }

                            }
                            t.Rollback();
                        }

                    }
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
                result.Msg = "取消订单出现异常";
            }
            return result;
        }

        public Result<MyOrderDetail> Detail(string iduser, string orderno)
        {
            Result<MyOrderDetail> result = new Result<MyOrderDetail>();
            try
            {
                if (!string.IsNullOrWhiteSpace(iduser) && !string.IsNullOrWhiteSpace(orderno))
                {
                    using (TN db = new TN())
                    {
                        TCom.EF.MyOrder o = db.MyOrders.Where(m => m.inuse == true && m.iduser == iduser && m.orderno == orderno).FirstOrDefault();
                        result.Data = new MyOrderDetail();
                        result.Data.Order = o;
                        result.Data.Presses = db.MyOrderPresses.Where(m => m.inuse == true && m.orderno == orderno).ToList();
                        result.Code = R.Ok;
                    }
                }
                else
                {
                    result.Code = R.Error;
                    result.Msg = "没找到订单";
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
                result.Msg = "出现异常";
            }
            return result;
        }


        public Result<TCom.EF.MyOrder> DetailFoyPay(string iduser, string orderno)
        {
            Result<TCom.EF.MyOrder> result = new Result<TCom.EF.MyOrder>();
            try
            {
                if (!string.IsNullOrWhiteSpace(iduser) && !string.IsNullOrWhiteSpace(orderno))
                {
                    using (TN db = new TN())
                    {
                        TCom.EF.MyOrder o = db.MyOrders.Where(m => m.inuse == true && m.iduser == iduser && m.orderno == orderno).FirstOrDefault();
                        result.Data = o;
                        result.Code = R.Ok;
                    }
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
                result.Msg = "出现异常";
            }
            return result;
        }


        public Result<string> Review(ReviewData data)
        {
            Result<string> result = new Result<string>();
            result.Code = R.Error;
            result.Msg = "审核失败";
            try
            {
                if (!string.IsNullOrWhiteSpace(data.mgcode) && !string.IsNullOrWhiteSpace(data.iduser) && !string.IsNullOrWhiteSpace(data.orderno))
                {
                    using (TN db = new TN())
                    {
                        TCom.EF.ManageUser mu = db.ManageUsers.Where(m => m.ManageUserId == data.mgcode).FirstOrDefault();
                        if (mu != null)
                        {
                            TCom.EF.MyOrder mo = db.MyOrders.Where(m => m.orderno == data.orderno && m.iduser == data.iduser).FirstOrDefault();
                            if (mo.status != OrderStatus.WaitReview)
                            {
                                result.Code = R.Error;
                                result.Msg = "审核失败,订单可能已经审核.";
                                return result;
                            }
                            TCom.EF.User uo = db.Users.Where(m => m.iduser == data.iduser).FirstOrDefault();
                            if (mo != null && uo != null)
                            {
                                using (DbContextTransaction t = db.Database.BeginTransaction())
                                {
                                    int stu = data.review ? OrderStatus.WaitPay : OrderStatus.ReviewFail;
                                    int r = db.Database.ExecuteSqlCommand("update myorder set status = {0} where iduser = {1} and orderno = {2} and status = {3}", stu, data.iduser, data.orderno, OrderStatus.WaitReview);
                                    if (r > 0)
                                    {
                                        stu = data.review ? OrderStatus.Confirm : OrderStatus.ReviewFail;
                                        MyOrderPress s = getMyOrderPress(data.orderno, stu, "职员 [" + mu.UserName + "]", data.content);
                                        db.MyOrderPresses.Add(s);
                                        MsgMgr.ReviewOrderResult(mo, uo, data.review, data.content, db);
                                        r = db.SaveChanges();
                                        if (r > 0)
                                        {
                                            t.Commit();
                                            MsgMgr.Post();
                                            result.Code = R.Ok;
                                            return result;
                                        }

                                    }
                                    else
                                    {
                                        result.Code = R.Error;
                                        result.Msg = "审核失败,订单可能已经审核.";
                                    }
                                    t.Rollback();
                                }
                            }
                            else
                            {
                                result.Code = R.Error;
                                result.Msg = "没找到订单";
                            }
                        }
                        else
                        {
                            result.Code = R.Error;
                            result.Msg = "无此职员";
                        }

                    }
                }
                else
                {
                    result.Code = R.Error;
                    result.Msg = "参数有误";
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
                result.Msg = "审核出现异常";
            }
            return result;
        }
    }
}
