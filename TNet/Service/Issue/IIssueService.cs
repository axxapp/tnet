using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TNet.Models.Issue;
using TNet.Models.Service.Com;
using TNet.Models.Task;

namespace TNet.Service.Issue
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IIssueService”。
    [ServiceContract]
    public interface IIssueService
    {
        //POST
        //注意:请求时必须指定请求头为以下类型，不然会报错
        //Accept: application/json
        //Content-Type: application/json

        /**************************************************/


        /// <summary>
        /// 创建问题
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = "Create", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        Result<string> Create(IssueData data);
        /// <summary>
        /// 获取指定用户的问题列表
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = "List/{iduser}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<List<IssueItem>> GetList(string iduser);



    }
}
