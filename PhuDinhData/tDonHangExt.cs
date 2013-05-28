using System;
using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tDonHang
    {
        public string TenDonHang
        {
            get
            {
                return string.Format("{0}_{1:hh\\:mm}_{2}", 
                    tChuyenHang.Ngay.ToShortDateString(), tChuyenHang.Gio ?? new TimeSpan(0, 0, 0, 0), rKhachHang.TenKhachHang);
            }
        }

        public tChuyenHang ChuyenHang { get; set; }
        public rKhachHang KhachHang { get; set; }
        public rBaiXe BaiXe { get; set; }
        public rChanh Chanh { get; set; }

        private static List<tChuyenHang> _tChuyenHang = new List<tChuyenHang>();
        public static List<tChuyenHang> tChuyenHangs
        {
            get { return _tChuyenHang; }
            set { _tChuyenHang = value; }
        }

        private static List<rKhachHang> _rKhachHangs = new List<rKhachHang>();
        public static List<rKhachHang> rKhachHangs
        {
            get { return _rKhachHangs; }
            set { _rKhachHangs = value; }
        }

        private static List<rBaiXe> _rBaiXes = new List<rBaiXe>();
        public static List<rBaiXe> rBaiXes
        {
            get { return _rBaiXes; }
            set { _rBaiXes = value; }
        }

        private static List<rChanh> _rChanhs = new List<rChanh>();
        public static List<rChanh> rChanhs
        {
            get { return _rChanhs; }
            set { _rChanhs = value; }
        }
    }
}
