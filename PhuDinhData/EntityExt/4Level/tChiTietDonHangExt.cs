using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChiTietDonHang
    {
        public string TenChiTietDonHang
        {
            get
            {
                return string.Format("{0}_{1}", tDonHang.TenDonHang, tMatHang.TenMatHangLoaiHang);
            }
        }

        public List<tDonHang> tDonHangList { get; set; }
        public List<tMatHang> tMatHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [DonHang {1}] [MatHang {2}] [SoLuong {3}] [Xong {4}]", Ma, tDonHang.TenDonHang, tMatHang.TenMatHang, SoLuong, Xong);
        }
    }
}
