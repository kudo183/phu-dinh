using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrPhuongTien
    {
        private static List<rPhuongTien> _rPhuongTiens;
        public static List<rPhuongTien> rPhuongTiens
        {
            get
            {
                if (_rPhuongTiens != null)
                {
                    return _rPhuongTiens;
                }

                const int count = 10;
                _rPhuongTiens = new List<rPhuongTien>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rPhuongTiens.Add(Create(i));
                }

                return _rPhuongTiens;
            }
        }

        public static rPhuongTien Create(int i)
        {
            return new rPhuongTien()
            {
                Ma = i,
                TenPhuongTien = "Phương tiện " + i
            };
        }
    }
}
