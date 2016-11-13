using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TNet.Models.Service.Com;

namespace TNet.Service.Ad
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IAdService”。
    [ServiceContract]
    public interface IAdService
    {

        /**************************************************/
        /// <summary>
        /// 广告
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = "List/{pos}/{city}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<List<TCom.EF.Advertise>> GetList(string pos, string city);
    }
}
