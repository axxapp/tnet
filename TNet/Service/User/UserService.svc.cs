using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using TCom.EF;
using TNet.Models.Service.Com;
using TNet.Models.User;

namespace TNet.Service.User
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserService.svc 或 UserService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class UserService : IUserService
    {
        public Result<bool> Update(UserData data)
        {
            Result<bool> result = new Result<bool>();
            if (data != null && !string.IsNullOrWhiteSpace(data.iduser) && !string.IsNullOrWhiteSpace(data.phone))
            {
                try
                {
                    long iduser = long.Parse(data.iduser);
                    using (TN db = new TN())
                    {
                        TCom.EF.User u = db.Users.Where(m => m.iduser == iduser).FirstOrDefault();
                        if (u != null)
                        {
                            if (u.alias == data.alias && u.phone == data.phone && u.comp == data.comp
                                 && u.sex == data.sex && u.notes == data.notes)
                            {
                                result.Msg = "用户信息未发生改变";
                                return result;

                            }
                            u.alias = data.alias;
                            u.phone = data.phone;
                            u.comp = data.comp;
                            u.sex = data.sex;
                            u.notes = data.notes;
                            if (db.SaveChanges() > 0)
                            {
                                result.Data = true;
                                result.Code = R.Ok;
                                result.Msg = "保存成功";

                            }
                            else
                            {
                                result.Code = R.Error;
                            }
                        }

                        // result.Data = m;
                    }
                }
                catch (Exception)
                {
                    result.Code = R.Error;
                    result.Msg = "出现异常";
                }
            }
            else
            {
                result.Msg = "参数有误";
            }
            return result;
        }

        
       
    }
}
