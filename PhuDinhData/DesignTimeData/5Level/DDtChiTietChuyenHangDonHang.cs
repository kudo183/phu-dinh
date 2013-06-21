using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiTietChuyenHangDonHang
    {
        private static List<tChiTietChuyenHangDonHang> _tChiTietChuyenHangDonHangs;
        public static List<tChiTietChuyenHangDonHang> tChiTietChuyenHangDonHangs
        {
            get
            {
                if (_tChiTietChuyenHangDonHangs != null)
                {
                    return _tChiTietChuyenHangDonHangs;
                }

                const int count = 10;
                _tChiTietChuyenHangDonHangs = new List<tChiTietChuyenHangDonHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _tChiTietChuyenHangDonHangs.Add(Create(i));
                }

                return _tChiTietChuyenHangDonHangs;
            }
        }

        public static tChiTietChuyenHangDonHang Create(int i)
        {
            return new tChiTietChuyenHangDonHang()
            {
                Ma = i,
                MaMatHang = i,
                MaChuyenHangDonHang = i,
                SoLuong = i * 10,
                tMatHang = DDtMatHang.Create(i),
                tChuyenHangDonHang = DDtChuyenHangDonHang.Create(i),
                tMatHangList = DDtMatHang.tMatHangs,
                tChuyenHangDonHangList = DDtChuyenHangDonHang.tChuyenHangDonHangs
            };
        }
    }
}
