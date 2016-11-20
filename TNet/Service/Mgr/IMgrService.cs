using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TNet.Models.Mgr;
using TNet.Models.Service.Com;

namespace TNet.Service.Mgr
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IMgrService”。
    [ServiceContract]
    public interface IMgrService
    {
        /// <summary>
        /// 获取工人
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = "Work/List", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<List<WorkerItem>> GetWorks();
    }
}
