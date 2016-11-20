using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using TCom.EF;
using TCom.Model.Task;
using TCom.Util;
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
                                var tp = db.TaskPresses.Where(m => m.idrecver == mro.idrecver && m.inuse == true && m.ptype == TaskPressType.Start).OrderByDescending(m => m.cretime).FirstOrDefault();
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
