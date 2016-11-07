using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TCom.Model.ToKey;
using TNet.Models.Service.Com;

namespace TNet.Service.ToKey
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IToKeyService”。
    [ServiceContract]
    public interface IToKeyService
    {
        //POST
        //注意:请求时必须指定请求头为以下类型，不然会报错
        //Accept: application/json
        //Content-Type: application/json

        /**************************************************/
        /// <summary>
        /// 微信令牌
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = "WeiXin", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        AccessToken WXToKey();
    }
}
