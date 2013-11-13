using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtNhapMatHang
    {
        private static List<tNhapMatHang> _tNhapMatHangs;
        public static List<tNhapMatHang> tNhapMatHangs
        {
            get
            {
                if (_tNhapMatHangs != null)
                {
                    return _tNhapMatHangs;
                }

                const int count = 10;
                _tNhapMatHangs = new List<tNhapMatHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _tNhapMatHangs.Add(Create(i));
                }

                return _tNhapMatHangs;
            }
        }

        public static tNhapMatHang Create(int i)
        {
            return new tNhapMatHang()
            {
                Ma = i,
                MaNhanVien = i,
                MaMatHang = i,
                Ngay = System.DateTime.Now,
                rNhanVien = DDrNhanVien.Create(i),
                rNhanVienList = DDrNhanVien.rNhanViens,
                tMatHang = DDtMatHang.Create(i),
                tMatHangList = DDtMatHang.tMatHangs
            };
        }
    }
}
