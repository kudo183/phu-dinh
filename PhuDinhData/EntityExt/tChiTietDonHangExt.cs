using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChiTietDonHang
    {
        private static List<tDonHang> _tDonHangs = new List<tDonHang>();
        public static List<tDonHang> tDonHangs
        {
            get { return _tDonHangs; }
            set
            {
                _tDonHangs.Clear();
                _tDonHangs.AddRange(value);
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
