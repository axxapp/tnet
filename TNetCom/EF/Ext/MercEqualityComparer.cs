using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCom.EF;

namespace TCom.EF {
    public class MercEqualityComparer : IEqualityComparer<Merc> {

        private MercEqualityComparer() { }
        public static MercEqualityComparer Instance = new MercEqualityComparer();
        public bool Equals(Merc x, Merc y) {
            return (x.idmerc == y.idmerc);
        }

        public int GetHashCode(Merc model) {
            return model.ToString().GetHashCode();
        }
    }
}
