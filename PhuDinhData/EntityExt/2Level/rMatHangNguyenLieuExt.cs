using System.Collections.Generic;

namespace PhuDinhData
{
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
