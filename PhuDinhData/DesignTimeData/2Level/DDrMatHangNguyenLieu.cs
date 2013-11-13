using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrMatHangNguyenLieu
    {
        private static List<rMatHangNguyenLieu> _rMatHangNguyenLieus;
        public static List<rMatHangNguyenLieu> rMatHangNguyenLieus
        {
            get
            {
                if (_rMatHangNguyenLieus != null)
                {
                    return _rMatHangNguyenLieus;
                }

                const int count = 10;
                _rMatHangNguyenLieus = new List<rMatHangNguyenLieu>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rMatHangNguyenLieus.Add(Create(i));
                }

                return _rMatHangNguyenLieus;
            }
        }

        public static rMatHangNguyenLieu Create(int i)
        {
            return new rMatHangNguyenLieu()
            {
                Ma = i,
                MaMatHang = i,
                MaNguyenLieu = i,
                tMatHang = DDtMatHang.Create(i),
                tMatHangList = DDtMatHang.tMatHangs,
                rNguyenLieu = DDrNguyenLieu.Create(i),
                rNguyenLieuList = DDrNguyenLieu.rNguyenLieus
            };
        }
    }
}
