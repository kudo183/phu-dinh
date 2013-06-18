using System;
using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChuyenHang
    {
        public string TenChuyenHang
        {
            get
            {
                return string.Format("{0}_{1:hh\\:mm}_{2}",
                    Ngay.ToShortDateString(), Gio ?? new TimeSpan(0, 0, 0, 0), rNhanVienGiaoHang.TenNhanVien);
            }
        }

        public List<rNhanVienGiaoHang> rNhanVienGiaoHangList { get; set; }
    }
}
