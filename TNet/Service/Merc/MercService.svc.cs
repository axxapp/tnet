using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using TCom.EF;
using TNet.Models.Merc;
using TNet.Models.Service.Com;
using TNet.Models.Service.Merc;

namespace TNet.Service.Merc
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MercService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 MercService.svc 或 MercService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class MercService : IMercService
    {
        public Result<MercInfo> GetAll()
        {
            Result<MercInfo> result = new Result<MercInfo>();
            try
            {
                using (TN db = new TN())
                {
                    MercInfo m = new MercInfo()
                    {
                        Merc = db.Mercs.Where(mr => mr.inuse == true).ToList(),
                        Spec = db.Specs.Where(mr => mr.inuse == true).ToList(),
                        Discount = db.Discounts.Where(mr => mr.inuse == true).ToList()
                    };
                    result.Data = m;
                    result.Code = R.Ok;
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
                result.Msg = "出现异常";
            }
            return result;
        }

        public Result<MercList> GetList(string city)
        {
            Result<MercList> result = new Result<MercList>();
            try
            {
                using (TN db = new TN())
                {
                    result.Data = new MercList()
                    {
                        Mercs = (from m in db.Mercs
                                 join c in db.CityRelations on m.idmerc equals c.idmodule
                                 where m.inuse == true && c.inuse == true && c.idcity == city
                                 orderby m.idtype descending, m.sortno descending
                                 select m).ToList(),

                        //db.Mercs.Where(mr => mr.inuse == true &&).OrderByDescending(m => m.idtype).ThenByDescending(m => m.sortno).ToList(),
                        Types = db.MercTypes.Where(m => m.inuse == true).OrderByDescending(m => m.sortno).ToList()

                    };
                    result.Code = R.Ok;
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
            }
            return result;

        }

        public Result<MercDataSingle> GetMercSingle(string idmerc)
        {
            Result<MercDataSingle> result = new Result<MercDataSingle>();
            try
            {
                if (!string.IsNullOrWhiteSpace(idmerc))
                {
                    using (TN db = new TN())
                    {
                        MercDataSingle m = new MercDataSingle()
                        {
                            Merc = db.Mercs.First(mr => mr.inuse == true && mr.idmerc == idmerc),
                            Spec = db.Specs.Where(mr => mr.inuse == true && mr.idmerc == idmerc).ToList(),
                            Discount = db.Discounts.Where(mr => mr.inuse == true && mr.idmerc == idmerc).ToList(),
                            Imgs = (from im in db.MercImages where (im.idmerc == idmerc) select im.Path).ToList()
                        };
                        if (m.Merc != null)
                        {
                            m.Setups = db.Setups.Where(mr => mr.inuse == true && mr.idtype == m.Merc.idtype).ToList();
                            m.SetupAddrs = db.SetupAddrs.Where(mr => mr.inuse == true && mr.idtype == m.Merc.idtype).ToList();
                        }
                        result.Data = m;
                        result.Code = R.Ok;
                    }
                }
            }
            catch (Exception e)
            {
                result.Code = R.Error;
                result.Msg = "出现异常" + e.Message;
            }
            return result;
        }



        public Result<SetupList> GetSetupList(string city)
        {
            Result<SetupList> result = new Result<SetupList>();
            try
            {
                using (TN db = new TN())
                {
                    result.Data = new SetupList()
                    {
                        Mercs = db.Mercs.Where(m => m.inuse == true && m.isetup == true).OrderByDescending(m => m.idtype).ThenByDescending(m => m.sortno).ToList(),
                        Types = db.MercTypes.Where(m => m.inuse == true).OrderByDescending(m => m.sortno).ToList(),
                        Setups = db.Setups.Where(m => m.inuse == true).ToList(),
                        SetupAddrs = db.SetupAddrs.Where(m => m.inuse == true).ToList(),
                    };
                    result.Code = R.Ok;
                }
            }
            catch (Exception)
            {
                result.Code = R.Error;
            }
            return result;

        }
    }
}
