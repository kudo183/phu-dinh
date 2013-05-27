using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChuyenHang
    {
        private static List<rNhanVienGiaoHang> _rNhanVienGiaoHangs = new List<rNhanVienGiaoHang>();
        public static List<rNhanVienGiaoHang> rNhanVienGiaoHangs
        {
            get { return _rNhanVienGiaoHangs; }
            set { _rNhanVienGiaoHangs = value; }
        }
    }
}
