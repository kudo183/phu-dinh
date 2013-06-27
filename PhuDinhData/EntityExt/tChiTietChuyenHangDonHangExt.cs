using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChiTietChuyenHangDonHang
    {
        public int SoLuongTheoDonHang
        {
            get
            {
                if (tChiTietDonHang != null)
                {
                    return tChiTietDonHang.SoLuong;
                }

                return 0;
            }
        }

        public List<tChuyenHangDonHang> tChuyenHangDonHangList { get; set; }
        public List<tChiTietDonHang> tChiTietDonHangList { get; set; }
    }
}
