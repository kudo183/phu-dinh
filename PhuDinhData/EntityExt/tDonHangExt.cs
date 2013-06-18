using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tDonHang
    {
        public string TenDonHang
        {
            get
            {
                return string.Format("{0}_{1}",
                    Ngay.ToShortDateString(), rKhachHang.TenKhachHang);
            }
        }

        public List<rKhachHang> rKhachHangList { get; set; }
        public List<rChanh> rChanhList { get; set; }
    }
}
