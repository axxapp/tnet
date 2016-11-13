﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TNet.Models.Business;
using TNet.Models.Service.Com;

namespace TNet.Service.Buss
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IBussService”。
    [ServiceContract]
    public interface IBussService
    {
        //POST
        //注意:请求时必须指定请求头为以下类型，不然会报错
        //Accept: application/json
        //Content-Type: application/json



        [WebInvoke(Method = "GET", UriTemplate = "List/{city}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<List<TCom.EF.Business>> List(string city);



        [WebInvoke(Method = "GET", UriTemplate = "Detail/{idbuss}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Result<BusinessDetail> Detail(string idbuss);
    }
}
