using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public class DDtChuyenHangDonHang
    {
        private static List<tChuyenHangDonHang> _tChuyenHangDonHangs;
        public static List<tChuyenHangDonHang> tChuyenHangDonHangs
        {
            get
            {
                if (_tChuyenHangDonHangs != null)
                {
                    return _tChuyenHangDonHangs;
                }

                const int count = 10;
                _tChuyenHangDonHangs = new List<tChuyenHangDonHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _tChuyenHangDonHangs.Add(Create(i));
                }

                return _tChuyenHangDonHangs;
            }
        }

        public static tChuyenHangDonHang Create(int i)
        {
            return new tChuyenHangDonHang()
            {
                Ma = i,
                MaChuyenHang = i,
                MaDonHang = i,
                tChuyenHang = DDtChuyenHang.Create(i),
                tDonHang = DDtDonHang.Create(i),
                tChuyenHangList = DDtChuyenHang.tChuyenHangs,
                tDonHangList = DDtDonHang.tDonHangs
            };
        }
    }
}
