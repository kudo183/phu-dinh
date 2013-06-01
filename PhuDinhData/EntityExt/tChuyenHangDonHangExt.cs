using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChuyenHangDonHang
    {
        public string TenChuyenHangDonHang
        {
            get { return string.Format("{0}_{1}", tChuyenHang.TenChuyenHang, tDonHang.TenDonHang); }
        }

        public tChuyenHang ChuyenHang { get; set; }
        public tDonHang DonHang { get; set; }

        private static List<tChuyenHang> _tChuyenHangs = new List<tChuyenHang>();
        public static List<tChuyenHang> tChuyenHangs
        {
            get { return _tChuyenHangs; }
            set { _tChuyenHangs = value; }
        }

        private static List<tDonHang> _tDonHangs = new List<tDonHang>();
        public static List<tDonHang> tDonHangs
        {
            get { return _tDonHangs; }
            set { _tDonHangs = value; }
        }
    }
}
