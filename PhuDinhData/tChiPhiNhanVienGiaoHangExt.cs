using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChiPhiNhanVienGiaoHang
    {
        public rNhanVienGiaoHang NhanVienGiaoHang { get; set; }
        public rLoaiChiPhi LoaiChiPhi { get; set; }

        private static List<rNhanVienGiaoHang> _rNhanVienGiaoHangs = new List<rNhanVienGiaoHang>();
        public static List<rNhanVienGiaoHang> rNhanVienGiaoHangs
        {
            get { return _rNhanVienGiaoHangs; }
            set { _rNhanVienGiaoHangs = value; }
        }

        private static List<rLoaiChiPhi> _rLoaiChiPhis = new List<rLoaiChiPhi>();
        public static List<rLoaiChiPhi> rLoaiChiPhis
        {
            get { return _rLoaiChiPhis; }
            set { _rLoaiChiPhis = value; }
        }
    }
}
