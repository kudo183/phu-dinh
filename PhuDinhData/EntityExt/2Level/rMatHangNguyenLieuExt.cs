using System.Collections.Generic;

namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rMatHangNguyenLieus")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("")]
    public partial class rMatHangNguyenLieu
    {
        public List<rNguyenLieu> rNguyenLieuList { get; set; }
        public List<tMatHang> tMatHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [NguyenLieu {1}] [MatHang {2}]", Ma
                , rNguyenLieu == null ? "" : rNguyenLieu.TenNguyenLieu
                , tMatHang == null ? "" : tMatHang.TenMatHang);
        }
    }
}
