using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChiTietChuyenHangDonHang
    {
        private static List<tChuyenHangDonHang> _tChuyenHangDonHangs = new List<tChuyenHangDonHang>();
        public static List<tChuyenHangDonHang> tChuyenHangDonHangs
        {
            get { return _tChuyenHangDonHangs; }
            set
            {
                _tChuyenHangDonHangs.Clear();
                _tChuyenHangDonHangs.AddRange(value);
            }
        }

        private static List<tMatHang> _tMatHangs = new List<tMatHang>();
        public static List<tMatHang> tMatHangs
        {
            get { return _tMatHangs; }
            set
            {
                _tMatHangs.Clear();
                _tMatHangs.AddRange(value);
            }
        }
    }
}
