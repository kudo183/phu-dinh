using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrKhachHang
    {
        private static List<rKhachHang> _rKhachHangs;
        public static List<rKhachHang> rKhachHangs
        {
            get
            {
                if (_rKhachHangs != null)
                {
                    return _rKhachHangs;
                }

                const int count = 10;
                _rKhachHangs = new List<rKhachHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rKhachHangs.Add(Create(i));
                }

                return _rKhachHangs;
            }
        }

        public static rKhachHang Create(int i)
        {
            return new rKhachHang()
            {
                Ma = i,
                MaDiaDiem = i,
                TenKhachHang = "Khách hàng " + i,
                rDiaDiem = DDrDiaDiem.Create(i),
                rDiaDiemList = DDrDiaDiem.rDiaDiems
            };
        }
    }
}
