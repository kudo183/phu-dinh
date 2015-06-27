using System.Collections.Generic;

namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rNhanViens")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("")]
    public partial class rNhanVien
    {
        public List<rPhuongTien> rPhuongTienList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [PhuongTien {1}] [TenNhanVien {2}]", Ma
                , rPhuongTien == null ? "" : rPhuongTien.TenPhuongTien
                , TenNhanVien);
        }
    }
}
