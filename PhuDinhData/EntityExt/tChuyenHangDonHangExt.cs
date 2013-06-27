﻿using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData
{
    public partial class tChuyenHangDonHang
    {
        public string TenChuyenHangDonHang
        {
            get { return string.Format("{0}_{1}", tChuyenHang.TenChuyenHang, tDonHang.TenDonHang); }
        }

        public int TongSoLuongTheoDonHang
        {
            get
            {
                int result = 0;

                if(tDonHang!=null)
                {
                    result += tDonHang.tChiTietDonHangs.Sum(tChiTietDonHang => tChiTietDonHang.SoLuong);
                }

                return result;
            }
        }

        public int TongSoLuongThucTe
        {
            get
            {
                int result = 0;
                if (tChiTietChuyenHangDonHangs != null)
                {
                    result += tChiTietChuyenHangDonHangs.Sum(tChiTietChuyenHangDonHang => tChiTietChuyenHangDonHang.SoLuong);
                }
                return result;
            }
        }

        public List<tChuyenHang> tChuyenHangList { get; set; }
        public List<tDonHang> tDonHangList { get; set; }
    }
}
