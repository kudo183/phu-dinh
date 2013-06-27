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
    }
}
