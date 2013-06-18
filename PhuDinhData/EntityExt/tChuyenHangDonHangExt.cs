using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChuyenHangDonHang
    {
        public string TenChuyenHangDonHang
        {
            get { return string.Format("{0}_{1}", tChuyenHang.TenChuyenHang, tDonHang.TenDonHang); }
        }

        public List<tChuyenHang> tChuyenHangList { get; set; }
        public List<tDonHang> tDonHangList { get; set; }
    }
}
