using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCom.EF;
using TNet.Models;

namespace TNet.BLL {
    public class WeiXinModuleService {

        public static List<WeiXinModule> GetALL() {
            TN db = new TN();
            return db.WeiXinModules.OrderByDescending(en => en.title).ToList();
        }

        public static WeiXinModule Get(string idwxmodule) {
            TN db = new TN();
            List<WeiXinModule> modules = db.WeiXinModules.Where(en => en.idwxmodule == idwxmodule).ToList();
            return (modules != null && modules.Count > 0) ? modules.First() : null;
        }

        public static WeiXinModule Edit(WeiXinModule module) {
            TN db = new TN();
            WeiXinModule oldModule = db.WeiXinModules.Where(en => en.idwxmodule == module.idwxmodule).FirstOrDefault();

            oldModule.idwxmodule = module.idwxmodule;
            //oldModule.name = module.name;
            //oldModule.code = module.code;
            oldModule.title = module.title;
            oldModule.ico = module.ico;
            oldModule.pubtime = module.pubtime;
            oldModule.endtime = module.endtime;
            oldModule.notes = module.notes;
            oldModule.inuse = module.inuse;

            
            db.SaveChanges();
            return oldModule;
        }

        public static WeiXinModule Add(WeiXinModule module) {
            TN db = new TN();
            db.WeiXinModules.Add(module);
            db.SaveChanges();
            return module;
        }
    }
}