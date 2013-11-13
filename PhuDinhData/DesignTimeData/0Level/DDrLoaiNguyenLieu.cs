using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrLoaiNguyenLieu
    {
        private static List<rLoaiNguyenLieu> _rLoaiNguyenLieus;
        public static List<rLoaiNguyenLieu> rLoaiNguyenLieus
        {
            get
            {
                if (_rLoaiNguyenLieus != null)
                {
                    return _rLoaiNguyenLieus;
                }

                const int count = 10;
                _rLoaiNguyenLieus = new List<rLoaiNguyenLieu>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rLoaiNguyenLieus.Add(Create(i));
                }

                return _rLoaiNguyenLieus;
            }
        }

        public static rLoaiNguyenLieu Create(int i)
        {
            return new rLoaiNguyenLieu()
            {
                Ma = i,
                TenLoai = "Loại nguyên liệu " + i
            };
        }
    }
}
