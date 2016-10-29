using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCom.EF {
    public class NoticeEqualityComparer : IEqualityComparer<Notice> {
        private NoticeEqualityComparer() { }
        public static NoticeEqualityComparer Instance = new NoticeEqualityComparer();
        public bool Equals(Notice x, Notice y) {
            return (x.idnotice == y.idnotice);
        }

        public int GetHashCode(Notice model) {
            return model.ToString().GetHashCode();
        }
    }
}
