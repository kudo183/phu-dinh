using System;
using System.Collections.Generic;
using System.Linq;

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

        public int TongDonHang
        {
            get { return tChuyenHangDonHangs.Count; }
        }

        public int TongSoLuongTheoDonHang
        {
            get
            {
                int result = 0;
                if (tChuyenHangDonHangs != null)
                {
                    result += tChuyenHangDonHangs.Where(tChuyenHangDonHang => tChuyenHangDonHang.tDonHang != null).
                        Sum(tChuyenHangDonHang => tChuyenHangDonHang.tDonHang.tChiTietDonHangs.
                            Sum(tChiTietDonHang => tChiTietDonHang.SoLuong));
                }
                return result;
            }
        }

        public int TongSoLuongThucTe
        {
            get
            {
                int result = 0;
                if (tChuyenHangDonHangs != null)
                {
                    result += tChuyenHangDonHangs.Where(tChuyenHangDonHang => tChuyenHangDonHang.tChiTietChuyenHangDonHangs != null).
                        Sum(tChuyenHangDonHang => tChuyenHangDonHang.tChiTietChuyenHangDonHangs.
                            Sum(tChiTietChuyenHangDonHang => tChiTietChuyenHangDonHang.SoLuong));
                }
                return result;
            }
        }

        public List<rNhanVienGiaoHang> rNhanVienGiaoHangList { get; set; }
    }
}
