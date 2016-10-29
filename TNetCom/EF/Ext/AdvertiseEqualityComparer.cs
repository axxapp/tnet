using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCom.EF {
    public class AdvertiseEqualityComparer : IEqualityComparer<Advertise> {
        private AdvertiseEqualityComparer() { }
        public static AdvertiseEqualityComparer Instance = new AdvertiseEqualityComparer();
        public bool Equals(Advertise x, Advertise y) {
            return (x.idav == y.idav);
        }

        public int GetHashCode(Advertise model) {
            return model.ToString().GetHashCode();
        }
    }
}
