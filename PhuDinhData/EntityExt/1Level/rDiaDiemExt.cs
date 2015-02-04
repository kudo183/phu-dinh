using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rDiaDiem
    {
        public string TenDiaDiem
        {
            get { return string.Format("{0}_{1}", rNuoc == null ? "" : rNuoc.TenNuoc, Tinh); }
        }

        public List<rNuoc> rNuocList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Tinh {1}] [Nuoc {2}]", Ma, Tinh
                , rNuoc == null ? "" : rNuoc.TenNuoc);
        }
    }
}
