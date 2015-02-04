using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChiTietChuyenKho
    {
        public List<tChuyenKho> tChuyenKhoList { get; set; }
        public List<tMatHang> tMatHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [DonHang {1}] [MatHang {2}] [SoLuong {3}]", Ma
                , tChuyenKho == null ? "" : tChuyenKho.TenChuyenKho
                , tMatHang == null ? "" : tMatHang.TenMatHang
                , SoLuong);
        }
    }
}
