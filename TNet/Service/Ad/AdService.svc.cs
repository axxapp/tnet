using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using TNet.Models.Service.Com;

namespace TNet.Service.Ad
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“AdService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 AdService.svc 或 AdService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AdService : IAdService
    {
        public Result<List<TCom.EF.Advertise>> GetList(string pos, string city)
        {
            Result<List<TCom.EF.Advertise>> result = new Result<List<TCom.EF.Advertise>>();
            if (!string.IsNullOrWhiteSpace(pos) && !string.IsNullOrWhiteSpace(city))
            {
                result.Msg = "暂无广告";
                try
                {
                    using (TCom.EF.TN db = new TCom.EF.TN())
                    {
                        result.Data = (from a in db.Advertises
                                       join at in db.AdvertiseTypes on a.idat equals at.idat
                                       join c in db.CityRelations on a.idav equals c.idmodule
                                       where at.pos == pos && c.idcity == city && c.inuse == true &&
                                       at.inuse == true && a.inuse == true
                                       orderby a.sortno
                                       select a).ToList();

                        if (result.Data != null)
                        {
                            result.Msg = "检索到 " + result.Data.Count + " 条广告";
                        }
                        result.Code = R.Ok;
                    }
                }
                catch (Exception)
                {
                    result.Code = R.Error;
                    result.Msg = "获取广告错误";
                }
            }
            else
            {
                result.Msg = "位置 或 城市有误";
            }
            return result;
        }

    }
}
