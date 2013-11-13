using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNguyenLieu
    {
        private static List<rNguyenLieu> _rNguyenLieus;
        public static List<rNguyenLieu> rNguyenLieus
        {
            get
            {
                if (_rNguyenLieus != null)
                {
                    return _rNguyenLieus;
                }

                const int count = 10;
                _rNguyenLieus = new List<rNguyenLieu>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rNguyenLieus.Add(Create(i));
                }
                return _rNguyenLieus;
            }
        }

        public static rNguyenLieu Create(int i)
        {
            return new rNguyenLieu()
            {
                Ma = i,
                MaLoaiNguyenLieu = i,
                DuongKinh = i,
                rLoaiNguyenLieu = DDrLoaiNguyenLieu.Create(i),
                rLoaiNguyenLieuList = DDrLoaiNguyenLieu.rLoaiNguyenLieus
            };
        }
    }
}
