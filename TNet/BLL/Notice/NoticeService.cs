using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCom.EF;
using TNet.Models;

namespace TNet.BLL {
    public class NoticeService {

        public static List<Notice> GetALL() {
            TN db = new TN();
            return db.Notices.OrderByDescending(en=>en.publish_time).ToList();
        }


        public static List<Notice> Search(string title="",string idcity="") {
            List<Notice> notices = new List<Notice>();
            TN db = new TN();
            notices = (from no in db.Notices
                     join cr in db.CityRelations on new { idnotice = no.idnotice, moduletype = (int)ModuleType.Notice } equals new { idnotice = cr.idmodule, moduletype = (cr.moduletype == null ? 0 : cr.moduletype.Value) }
                       orderby no.sortno descending
                     where (string.IsNullOrEmpty(idcity) || (cr.idcity == idcity))
                     select no).Where(en => (
              (string.IsNullOrEmpty(title) || en.title.Contains(title))
             )).ToList();
            return notices.Distinct(NoticeEqualityComparer.Instance).ToList();
        }

        public static Notice Get(string idnotice) {
            return GetALL().Where(en => en.idnotice == idnotice).FirstOrDefault();
        }

        public static Notice Edit(Notice notice) {
            TN db = new TN();
            Notice oldNotice = db.Notices.Where(en => en.idnotice == notice.idnotice).FirstOrDefault();

            oldNotice.idnotice = notice.idnotice;
            oldNotice.publish = notice.publish;
            oldNotice.title = notice.title;
            //oldNotice.publish_time = notice.publish_time;
            oldNotice.start_time = notice.start_time;
            oldNotice.end_time = notice.end_time;
            oldNotice.content = notice.content;
            oldNotice.sortno = notice.sortno;

            db.SaveChanges();
            return oldNotice;
        }

        public static Notice Add(Notice notice) {
            TN db = new TN();
            db.Notices.Add(notice);
            db.SaveChanges();
            return notice;
        }


        public static bool Delete(List<string> idnotices) {
            int count = 0;
            TN db = new TN();
            for (int i = 0; i < idnotices.Count; i++) {
                Notice notice = db.Notices.Remove(db.Notices.Find(idnotices[i]));
                if (notice != null) {
                    count++;
                }
            }
            db.SaveChanges();
            return count > 0 ? true : false;
        }
    }
}