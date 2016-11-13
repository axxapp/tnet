using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using TCom.EF;
using TNet.Models.Business;
using TNet.Models.Service.Com;

namespace TNet.Service.Buss
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“BussService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 BussService.svc 或 BussService.svc.cs，然后开始调试。

    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class BussService : IBussService
    {
        public Result<List<TCom.EF.Business>> List(string city)
        {
            Result<List<TCom.EF.Business>> result = new Result<List<TCom.EF.Business>>();
            try
            {
                using (TN db = new TN())
                {
                    result.Data = db.Businesses.Where(mr => mr.inuse == true && mr.citycode == city).OrderByDescending(mr => mr.sortno).ToList();
                    result.Code = R.Ok;
                }
            }
            catch (Exception)
            {
                result.Msg = "获取周边商圈错误";
                result.Code = R.Error;
            }
            return result;
        }


        public Result<BusinessDetail> Detail(string idbuss)
        {
            Result<BusinessDetail> result = new Result<BusinessDetail>();
            try
            {
                if (!string.IsNullOrWhiteSpace(idbuss))
                {
                    using (TN db = new TN())
                    {
                        result.Data = new BusinessDetail();
                        result.Data.Buss = db.Businesses.Where(m => m.inuse == true && m.idbuss == idbuss).FirstOrDefault();
                        result.Data.Imgs = db.BussImages.Where(m => m.idbuss == idbuss && m.InUse == true).Select(m=>m.Path).ToList();
                        result.Code = R.Ok;
                    }
                }
            }
            catch (Exception)
            {
                result.Msg = "获取商家错误";
                result.Code = R.Error;
            }
            return result;
        }
    }
}
