using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNhanVienGiaoHang
    {
        private static List<rNhanVienGiaoHang> _rNhanVienGiaoHangs;
        public static List<rNhanVienGiaoHang> rNhanVienGiaoHangs
        {
            get
            {
                if (_rNhanVienGiaoHangs != null)
                {
                    return _rNhanVienGiaoHangs;
                }

                const int count = 10;
                _rNhanVienGiaoHangs = new List<rNhanVienGiaoHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rNhanVienGiaoHangs.Add(Create(i));
                }
                return _rNhanVienGiaoHangs;
            }
        }

        public static rNhanVienGiaoHang Create(int i)
        {
            return new rNhanVienGiaoHang()
            {
                Ma = i,
                MaPhuongTien = i,
                TenNhanVien = "Nhân viên " + i,
                rPhuongTien = DDrPhuongTien.Create(i),
                rPhuongTienList = DDrPhuongTien.rPhuongTiens
            };
        }
    }
}
