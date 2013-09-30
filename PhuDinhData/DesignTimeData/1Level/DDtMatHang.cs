using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtMatHang
    {
        private static List<tMatHang> _tMatHangs;
        public static List<tMatHang> tMatHangs
        {
            get
            {
                if (_tMatHangs != null)
                {
                    return _tMatHangs;
                }

                const int count = 10;
                _tMatHangs = new List<tMatHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _tMatHangs.Add(Create(i));
                }
                return _tMatHangs;
            }
        }

        public static tMatHang Create(int i)
        {
            return new tMatHang()
            {
                Ma = i,
                MaLoai = i,
                TenMatHang = "Mặt hàng " + i,
                SoKy = i,
                SoMet = i * 10,
                rLoaiHang = DDrLoaiHang.Create(i),
                rLoaiHangList = DDrLoaiHang.rLoaiHangs
            };
        }
    }
}
