using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrLoaiHang
    {
        private static List<rLoaiHang> _rLoaiHangs;
        public static List<rLoaiHang> rLoaiHangs
        {
            get
            {
                if (_rLoaiHangs != null)
                {
                    return _rLoaiHangs;
                }

                const int count = 10;
                _rLoaiHangs = new List<rLoaiHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rLoaiHangs.Add(Create(i));
                }

                return _rLoaiHangs;
            }
        }

        public static rLoaiHang Create(int i)
        {
            return new rLoaiHang()
            {
                Ma = i,
                TenLoai = "Loại hàng " + i
            };
        }
    }
}
