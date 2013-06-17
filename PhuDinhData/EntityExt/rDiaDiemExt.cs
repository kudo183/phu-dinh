using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rDiaDiem
    {
        public string TenDiaDiem
        {
            get { return string.Format("{0}_{1}", rNuoc.TenNuoc, Tinh); }
        }

        public List<rNuoc> rNuocList { get; set; }
    }
}
