using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNhanVien
    {
        private static List<rNhanVien> _rNhanViens;
        public static List<rNhanVien> rNhanViens
        {
            get
            {
                if (_rNhanViens != null)
                {
                    return _rNhanViens;
                }

                const int count = 10;
                _rNhanViens = new List<rNhanVien>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rNhanViens.Add(Create(i));
                }
                return _rNhanViens;
            }
        }

        public static rNhanVien Create(int i)
        {
            return new rNhanVien()
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
