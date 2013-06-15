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

        public rNhanVienGiaoHang NhanVienGiaoHang { get; set; }

        private static List<rNhanVienGiaoHang> _rNhanVienGiaoHangs = new List<rNhanVienGiaoHang>();
        public static List<rNhanVienGiaoHang> rNhanVienGiaoHangs
        {
            get { return _rNhanVienGiaoHangs; }
            set
            {
                _rNhanVienGiaoHangs.Clear();
                _rNhanVienGiaoHangs.AddRange(value);
            }
        }
    }
}
