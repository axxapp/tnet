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
using TNet.Models.Issue;
using TNet.Models.Service.Com;
using TNet.Models.Task;

namespace TNet.Service.Issue
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“IssueService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 IssueService.svc 或 IssueService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class IssueService : IIssueService
    {

        public Result<string> Create(IssueData data)
        {
            Result<string> result = new Result<string>();
            result.Msg = "受理失败";
            if (data != null)
            {
                try
                {
                    DateTime bt = DateTime.Now.AddHours(4);
                    if (!string.IsNullOrWhiteSpace(data.booktime))
                    {
                        try
                        {
                            bt = DateTime.Parse(data.booktime);
                        }
                        catch (Exception)
                        {
                            result.Msg = "预约时间格式有误";
                            return result;
                        }
                    }
                    using (TCom.EF.TN db = new TCom.EF.TN())
                    {
                        TCom.EF.Issue u = new TCom.EF.Issue();
                        u.issue1 = Pub.ID();
                        u.iduser = data.iduser;
                        u.context = data.context;
                        u.cretime = DateTime.Now;
                        u.booktime = bt;
                        u.lng = data.lng;
                        u.lat = data.lng;
                        u.contact = data.contact;
                        u.addr = data.addr;
                        u.phone = data.phone;
                        u.notes = data.notes;
                        u.tasktype = TaskType.Complaint;
                        u.idtask = "";
                        u.inuse = true;
                        db.Issues.Add(u);
                        if (data.imgs != null && data.imgs.Count > 0)
                        {
                            for (int i = 0; i < data.imgs.Count; i++)
                            {
                                TCom.EF.Img mo = new TCom.EF.Img();
                                mo.idimg = Pub.ID();
                                mo.outkey = u.issue1;
                                mo.path = data.imgs[i];
                                mo.sortno = i;
                                mo.inuse = true;
                                db.Imgs.Add(mo);
                            }
                        }
                        MsgMgr.PostCreateIssue(u.issue1,db);
                        if (db.SaveChanges() > 0)
                        {
                            MsgMgr.Post();
                            result.Data = u.issue1;
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

        public Result<List<IssueItem>> GetList(string iduser)
        {
            Result<List<IssueItem>> result = new Result<List<IssueItem>>();
            try
            {
                using (TCom.EF.TN db = new TCom.EF.TN())
                {
                    result.Data = ((from u in db.Issues
                                    join t in db.Tasks on u.idtask equals t.idtask into ts
                                    where (u.iduser == iduser && u.inuse == true)
                                    from tt in ts.DefaultIfEmpty()
                                    orderby u.issue1 descending
                                    select new IssueItem()
                                    {
                                        Issue = u,
                                        _Task = tt

                                    })).ToList();
                    if (result.Data != null)
                    {
                        List<string> issue1s = new List<string>();
                        for (int i = 0; i < result.Data.Count; i++)
                        {
                            issue1s.Add(result.Data[i].Issue.issue1);
                        }
                        var imgs = (from m in db.Imgs where issue1s.Contains(m.outkey) orderby m.outkey ascending, m.sortno select m).ToList();
                        if (imgs != null && imgs.Count > 0)
                        {
                            for (int i = 0; i < result.Data.Count; i++)
                            {
                                IssueItem io = result.Data[i];
                                for (int j = imgs.Count - 1; j >= 0; j--)
                                {
                                    if (io.Issue.issue1 == imgs[j].outkey)
                                    {
                                        io.Imgs.Add(imgs[j].path);
                                        imgs.RemoveAt(j);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (imgs.Count <= 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    result.Code = R.Ok;
                    result.Msg = "检索到 " + result.Data.Count + " 条数据";
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
                result.Msg = "出现异常";
            }
            return result;

        }


        public Result<IssueDetail> Detail(string issue)
        {
            Result<IssueDetail> result = new Result<IssueDetail>();
            try
            {
                using (TCom.EF.TN db = new TCom.EF.TN())
                {
                    result.Data = new IssueDetail();
                    result.Data.issue = (from u in db.Issues
                                         where (u.issue1 == issue && u.inuse == true)
                                         select u).FirstOrDefault();

                    if (result.Data.issue != null)
                    {

                        var img = (from m in db.Imgs
                                   where m.outkey == issue
                                   orderby m.sortno
                                   select new TaskImg()
                                   {
                                       path = m.path,
                                       outkey = m.outkey,
                                       outpro = m.outpro,
                                       type = "main"
                                   }).ToList();

                        result.Data.imgs = new List<TaskImg>();
                        result.Data.imgs.AddRange(img);
                        string idtask = result.Data.issue.idtask;
                        if (!string.IsNullOrWhiteSpace(idtask))
                        {
                            result.Data.task = (from u in db.Tasks
                                                where u.idtask == result.Data.issue.idtask && u.inuse == true
                                                select new TaskDetailItem()
                                                {
                                                    _task = u
                                                }
                                                ).FirstOrDefault();
                            if (result.Data.task != null)
                            { 
                                var ps = (from u in db.TaskPresses
                                          where (u.idtask == result.Data.issue.idtask && u.inuse == true)
                                          orderby u.cretime
                                          select u).ToList();
                                result.Data.press = TaskPressItem.gets(ps);

                                var tr = (from u in db.TaskRecvers
                                          where (u.idtask == result.Data.issue.idtask && u.inuse == true)
                                          orderby u.cretime
                                          select u).ToList();
                                result.Data.recver = TaskRecverItem.gets(tr);

                                if (result.Data.press != null && result.Data.press.Count > 0)
                                {
                                    var pimgs = (from m in db.Imgs
                                                 where m.outpro2 == idtask
                                                 orderby m.sortno
                                                 select new TaskImg()
                                                 {
                                                     path = m.path,
                                                     outkey = m.outkey,
                                                     outpro = m.outpro,
                                                     type = "press"
                                                 }).ToList();
                                    result.Data.imgs.AddRange(pimgs); 
                                } 
                            }

                        }
                        result.Code = R.Ok;
                        result.Msg = "检索到到数据";
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






    }
}
