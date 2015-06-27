using System.Collections.Generic;

namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rDiaDiems")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("TenDiaDiem")]
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
