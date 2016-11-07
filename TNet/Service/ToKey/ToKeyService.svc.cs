﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TCom.Model.ToKey;
using TCom.Util;
using TNet.Models.Service.Com;

namespace TNet.Service.ToKey
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ToKeyService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ToKeyService.svc 或 ToKeyService.svc.cs，然后开始调试。
    public class ToKeyService : IToKeyService
    {
        public AccessToken WXToKey()
        {
            return Pub.accessTokenObj;

        }
    }
}
