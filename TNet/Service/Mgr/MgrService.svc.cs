using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web.Util;
using TNet.Models.Mgr;
using TNet.Models.Service.Com;

namespace TNet.Service.Mgr
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MgrService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 MgrService.svc 或 MgrService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class MgrService : IMgrService
    {
        public Result<List<WorkerItem>> GetWorks()
        {
            Result<List<WorkerItem>> result = new Result<List<WorkerItem>>();
            result.Msg = "暂无工人";
            try
            {
                using (TCom.EF.TN db = new TCom.EF.TN())
                {
                    result.Data = db.ManageUsers.Where(m => m.inuse == true && m.recv_setup == true).Select(m => new WorkerItem()
                    {
                        mgcode = m.ManageUserId,
                        mgname = m.UserName
                    }).ToList();
                    result.Code = R.Ok;
                    if (result.Data != null && result.Data.Count > 0)
                    {
                        result.Msg = "检索到 " + result.Data.Count + " 位工人";
                    }
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
                result.Msg = "获取工人出错";
            }
            return result;
        }
    }
}
