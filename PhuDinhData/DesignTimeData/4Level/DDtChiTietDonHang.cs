using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiTietDonHang
    {
        private static List<tChiTietDonHang> _tChiTietDonHangs;
        public static List<tChiTietDonHang> tChiTietDonHangs
        {
            get
            {
                if (_tChiTietDonHangs != null)
                {
                    return _tChiTietDonHangs;
                }

                const int count = 10;
                _tChiTietDonHangs = new List<tChiTietDonHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _tChiTietDonHangs.Add(Create(i));
                }

                return _tChiTietDonHangs;
            }
        }

        public static tChiTietDonHang Create(int i)
        {
            return new tChiTietDonHang()
            {
                Ma = i,
                MaDonHang = i,
                MaMatHang = i,
                SoLuong = i * 10,
                tDonHang = DDtDonHang.Create(i),
                tMatHang = DDtMatHang.Create(i),
                tDonHangList = DDtDonHang.ViewModel.Entity.ToList(),
                tMatHangList = DDtMatHang.ViewModel.Entity.ToList()
            };
        }
    }
}
