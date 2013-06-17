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

        private static List<tChuyenHang> _tChuyenHang = new List<tChuyenHang>();
        public static List<tChuyenHang> tChuyenHangs
        {
            get { return _tChuyenHang; }
            set
            {
                _tChuyenHang.Clear();
                _tChuyenHang.AddRange(value);
            }
        }

        private static List<rKhachHang> _rKhachHangs = new List<rKhachHang>();
        public static List<rKhachHang> rKhachHangs
        {
            get { return _rKhachHangs; }
            set
            {
                _rKhachHangs.Clear();
                _rKhachHangs.AddRange(value);
            }
        }

        private static List<rChanh> _rChanhs = new List<rChanh>();
        public static List<rChanh> rChanhs
        {
            get { return _rChanhs; }
            set
            {
                _rChanhs.Clear();
                _rChanhs.AddRange(value);
            }
        }
    }
}
