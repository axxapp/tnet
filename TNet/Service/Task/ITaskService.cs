using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TNet.Models.Service.Com;
using TNet.Models.Task;

namespace TNet.Service.Task
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ITaskService”。
    [ServiceContract]
    public interface ITaskService
    {
        //POST
        //注意:请求时必须指定请求头为以下类型，不然会报错
        //Accept: application/json
        //Content-Type: application/json

        /**************************************************/
        /// <summary>
        /// 所有商品信息,全部信息
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = "List/{mgcode}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<List<TaskItem>> List(string mgcode);


        [WebInvoke(Method = "GET", UriTemplate = "Detail/{idtask}/{idrecver}/{mgcode}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<TaskDetail> Detail(string idtask, string idrecver, string mgcode);

        /// <summary>
        /// 投诉报修派单
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = "Dis/issue", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<string> DisIssue(DisTaskIssueData data);

        /// <summary>
        /// 订单派单
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = "Dis/Order", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<string> DisOrder(DisTaskOrderData data);


        /// <summary>
        /// 开工
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = "Start", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<string> Start(StartData data);

        /// <summary>
        /// 暂结
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = "Pause", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<string> Pause(TaskPauseData data);

        /// <summary>
        /// 完工
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = "Finish", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<string> Finish(TaskFinishData data);



    }
}
