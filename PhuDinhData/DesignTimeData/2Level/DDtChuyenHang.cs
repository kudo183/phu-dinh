using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChuyenHang
    {
        private static List<tChuyenHang> _tChuyenHangs;
        public static List<tChuyenHang> tChuyenHangs
        {
            get
            {
                if (_tChuyenHangs != null)
                {
                    return _tChuyenHangs;
                }

                const int count = 10;
                _tChuyenHangs = new List<tChuyenHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _tChuyenHangs.Add(Create(i));
                }

                return _tChuyenHangs;
            }
        }

        public static tChuyenHang Create(int i)
        {
            return new tChuyenHang()
            {
                Ma = i,
                MaNhanVienGiaoHang = i,
                Ngay = System.DateTime.Now,
                Gio = System.DateTime.Now.TimeOfDay,
                rNhanVienGiaoHang = DDrNhanVienGiaoHang.Create(i),
                rNhanVienGiaoHangList = DDrNhanVienGiaoHang.rNhanVienGiaoHangs
            };
        }
    }
}
