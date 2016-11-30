using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using TCom.EF;
using TCom.Model.Task;
using TCom.Msg;
using TCom.Util;
using TNet.Models.Mgr;
using TNet.Models.Service.Com;
using TNet.Models.Task;

namespace TNet.Service.Task
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“TaskService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 TaskService.svc 或 TaskService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class TaskService : ITaskService
    {
        public Result<List<TaskItem>> List(string mgcode)
        {

            Result<List<TaskItem>> result = new Result<List<TaskItem>>();
            try
            {
                if (!string.IsNullOrWhiteSpace(mgcode))
                {

                    using (TN db = new TN())
                    {
                        List<TCom.EF.Task> ts = (from t in db.Tasks
                                                 where ((from tr in db.TaskRecvers
                                                         where tr.mcode == mgcode
                                                         select tr.idtask).Contains(t.idtask))
                                                 select t).ToList();
                        result.Data = TaskItem.gets(ts);

                        result.Code = R.Ok;
                    }
                }
            }
            catch (Exception e)
            {
                result.Code = R.Error;
                result.Msg = "出现异常" + e.Message;
            }
            return result;
        }

        public Result<TaskDetail> Detail(string idtask, string mgcode)
        {
            Result<TaskDetail> result = new Result<TaskDetail>();
            result.Msg = "暂无工单";
            if (!string.IsNullOrWhiteSpace(idtask) && !string.IsNullOrWhiteSpace(mgcode))
            {
                try
                {
                    using (TCom.EF.TN db = new TCom.EF.TN())
                    {
                        result.Data = new TaskDetail();
                        TCom.EF.Task ts = (from t in db.Tasks where t.inuse == true && t.idtask == idtask select t).FirstOrDefault();
                        if (ts != null)
                        {
                            result.Data.task = TaskItem.get(ts);
                            List<TCom.EF.TaskPress> ps = (from t in db.TaskPresses where t.inuse == true && t.idtask == idtask orderby t.cretime select t).ToList();
                            result.Data.press = TaskPressItem.gets(ps);
                            List<TCom.EF.TaskRecver> tr = (from t in db.TaskRecvers where t.inuse == true && t.idtask == idtask orderby t.cretime select t).ToList();
                            result.Data.recver = TaskRecverItem.gets(tr);
                            var pm = (from m in db.Imgs
                                      where m.inuse == true && m.outpro2 == ts.idtask
                                      select new TaskImg()
                                      {
                                          path = m.path,
                                          outkey = m.outkey,
                                          outpro = m.outpro,
                                          type = "press"
                                      }).ToList();
                            result.Data.imgs = new List<TaskImg>();
                            result.Data.imgs.AddRange(pm);

                            if (ts.tasktype == TaskType.Complaint)//投诉业务-问题图片
                            {
                                var om = (from m in db.Imgs
                                          where m.inuse == true && m.outkey == ts.orderno
                                          select new TaskImg()
                                          {
                                              path = m.path,
                                              outkey = m.outkey,
                                              outpro = m.outpro,
                                              type = "issue"
                                          }).ToList();
                                result.Data.imgs.AddRange(om);
                            }
                            else if (ts.tasktype == TaskType.Setup)//报装业务-订单信息
                            {
                                result.Data.order = (from m in db.MyOrders
                                                     where m.inuse == true && m.orderno == ts.orderno
                                                     select new TaskOrder()
                                                     {
                                                         idmerc = m.idmerc,
                                                         merc = m.merc,
                                                         spec = m.spec,
                                                         price = m.price != null ? m.price.Value : 0,
                                                         count = m.count,
                                                         img = m.img
                                                     }).FirstOrDefault();

                            }
                        }
                        result.Code = R.Ok;
                        result.Msg = "检索到数据";
                    }
                }
                catch (Exception)
                {
                    result.Code = R.Error;
                    result.Msg = "拉取工单出错";

                }
            }
            else
            {
                result.Msg = "工单、职员编号不能为空";
            }
            return result;
        }


        public Result<string> DisTask(DisTaskOrderData data)
        {
            Result<string> result = new Result<string>();
            result.Msg = "派单失败";
            try
            {
                if (data != null && data.works != null && data.works.Count > 0)
                {
                    using (TCom.EF.TN r = new TCom.EF.TN())
                    {
                        TCom.EF.MyOrder os = r.MyOrders.Where(o => o.orderno == data.orderno && o.inuse == true).FirstOrDefault();
                        if (os != null)
                        {
                            if (!string.IsNullOrWhiteSpace(os.idtask))
                            {
                                TCom.EF.Task t = r.Tasks.Where(o => o.idtask == os.idtask && o.inuse == true && o.status != TaskStatus.Close).FirstOrDefault();
                                if (t != null)
                                {
                                    result.Msg = "该订单有正在进行的工单";
                                    return result;
                                }
                            }
                            TCom.EF.User uo = r.Users.Where(m => m.iduser == os.iduser && m.inuse == true).FirstOrDefault(); ;
                            if (uo != null)
                            {


                                TCom.EF.Task ts = new TCom.EF.Task();
                                ts.idtask = Pub.ID();
                                ts.iduser = os.iduser;
                                ts.name = uo.name;
                                ts.idsend = data.iduser;
                                ts.send = data.uname;
                                ts.orderno = data.orderno;
                                ts.accpeptime = os.cretime;
                                ts.cretime = DateTime.Now;
                                ts.revctime = DateTime.Now;
                                ts.status = TaskStatus.WaitPress;
                                ts.contact = os.contact;
                                ts.addr = os.addr;
                                ts.phone = os.phone;
                                ts.title = "报装" + os.merc + "--" + os.spec;
                                ts.text = "商品:" + os.merc + ",规格:" + os.spec;
                                ts.tasktype = TaskType.Setup;
                                ts.score = 0;
                                ts.notes = "";
                                ts.inuse = true;
                                r.Tasks.Add(ts);
                                List<ManageUser> ms = (from m in r.ManageUsers
                                                       where data.works.Contains(m.ManageUserId) && m.inuse == true
                                                       && m.recv_setup == true
                                                       select m).ToList();
                                if (ms != null && ms.Count > 0)
                                {
                                    for (int i = 0; i < ms.Count; i++)
                                    {
                                        var mo = ms[i];
                                        TCom.EF.TaskRecver tr = new TaskRecver();
                                        tr.idrecver = Pub.ID();
                                        tr.idtask = ts.idtask;
                                        tr.mcode = mo.ManageUserId;
                                        tr.mname = mo.UserName;
                                        tr.cretime = DateTime.Now;
                                        tr.stime = DateTime.Now;
                                        tr.works = 0;
                                        tr.donum = 0;
                                        tr.smcode = "";
                                        tr.smname = "";
                                        tr.status = WorkerStatus.Revice;
                                        ts.inuse = true;
                                        r.TaskRecvers.Add(tr);

                                        TCom.EF.TaskPress tp = new TaskPress();
                                        tp.idpress = Pub.ID();
                                        tp.idrecver = tr.mcode;
                                        tp.idtask = ts.idtask;
                                        tp.cretime = DateTime.Now;
                                        tp.ptext = "派单";
                                        tp.pdesc = "商品:" + os.merc + ",规格:" + os.spec;
                                        tp.ptype = TaskPressType.DisTask;
                                        tp.inuse = true;
                                        r.TaskPresses.Add(tp);
                                        MsgMgr.SetupOrder(ts.idtask, os, uo, mo, r);
                                    }
                                    r.MyOrders.Attach(os);
                                    os.idtask = ts.idtask;

                                    if (r.SaveChanges() > 0)
                                    {
                                        MsgMgr.Post();
                                        result.Code = R.Ok;
                                        result.Data = ts.idtask;
                                        result.Msg = "派单成功";
                                    }
                                }
                            }
                            else
                            {
                                result.Msg = "订单用户不存在";
                            }
                        }
                    }
                }
                else
                {
                    result.Msg = "工人不能为空";
                }
            }
            catch (Exception)
            {
                result.Msg = "派单发生错误";

            }
            return result;

        }
        public Result<string> Finish(TaskFinishData data)
        {
            Result<string> result = new Result<string>();
            result.Msg = "受理失败";
            if (data != null)
            {
                try
                {
                    using (TCom.EF.TN db = new TCom.EF.TN())
                    {
                        var mo = db.ManageUsers.Where(m => m.ManageUserId == data.mgcode && m.inuse == true).FirstOrDefault();
                        if (mo != null)
                        {

                            var to = db.Tasks.Where(m => m.idtask == data.idtask && m.inuse == true).FirstOrDefault();
                            if (to != null)
                            {
                                var mro = db.TaskRecvers.Where(m => m.mcode == data.mgcode && m.inuse == true).OrderByDescending(m => m.cretime).FirstOrDefault();
                                var tp = db.TaskPresses.Where(m => m.idrecver == mro.idrecver && m.inuse == true && m.ptype == TaskPressType.Doing).OrderByDescending(m => m.cretime).FirstOrDefault();
                                db.Tasks.Attach(to);

                                //变更接受工人信息
                                db.TaskRecvers.Attach(mro);
                                DateTime dt = tp.cretime != null ? tp.cretime.Value : DateTime.Now;
                                mro.works += (dt - DateTime.Now).TotalMinutes;
                                mro.status = TaskRecverStatus.Finish;
                                mro.donum++;

                                //记录工单处理
                                TCom.EF.TaskPress p = new TCom.EF.TaskPress();
                                p.idpress = Pub.ID();
                                p.idtask = data.idtask;
                                p.cretime = DateTime.Now;
                                p.ptext = "完工";
                                p.pdesc = data.context;
                                p.ptype = TaskPressType.Finish;
                                p.inuse = true;
                                db.TaskPresses.Add(p);

                                //新增图片
                                if (data.imgs != null && data.imgs.Count > 0)
                                {
                                    for (int i = 0; i < data.imgs.Count; i++)
                                    {
                                        TCom.EF.Img mgo = new TCom.EF.Img();
                                        mgo.idimg = Pub.ID();
                                        mgo.outkey = p.idpress;
                                        mgo.outpro = p.idtask;
                                        mgo.path = data.imgs[i];
                                        mgo.sortno = i;
                                        mgo.inuse = true;
                                        db.Imgs.Add(mgo);
                                    }
                                }
                            }
                        }


                        if (db.SaveChanges() > 0)
                        {
                            //  result.Data = u.issue1;
                            result.Code = R.Ok;
                            result.Msg = "提交成功,我们会尽快处理";
                        }
                    }
                }
                catch (Exception)
                {
                    result.Code = R.Error;
                    result.Msg = "出现异常";
                }
            }
            return result;
        }
    }
}
