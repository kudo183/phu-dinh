using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChuyenHang
    {
        private static List<rNhanVienGiaoHang> _rNhanVienGiaoHang = new List<rNhanVienGiaoHang>();
        public static List<rNhanVienGiaoHang> rNhanVienGiaoHangs
        {
            get { return _rNhanVienGiaoHang; }
            set { _rNhanVienGiaoHang = value; }
        }
    }
}
