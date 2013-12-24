using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtNhapNguyenLieu
    {
        private static List<tNhapNguyenLieu> _tNhapNguyenLieus;
        public static List<tNhapNguyenLieu> tNhapNguyenLieus
        {
            get
            {
                if (_tNhapNguyenLieus != null)
                {
                    return _tNhapNguyenLieus;
                }

                const int count = 10;
                _tNhapNguyenLieus = new List<tNhapNguyenLieu>(count);
                for (var i = 1; i <= count; i++)
                {
                    _tNhapNguyenLieus.Add(Create(i));
                }

                return _tNhapNguyenLieus;
            }
        }

        public static tNhapNguyenLieu Create(int i)
        {
            return new tNhapNguyenLieu()
            {
                Ma = i,
                MaNhaCungCap = i,
                MaNguyenLieu = i,
                Ngay = System.DateTime.Now,
                rNguyenLieu= DDrNguyenLieu.Create(i),
                rNguyenLieuList = DDrNguyenLieu.ViewModel.Entity.ToList(),
                rNhaCungCap = DDrNhaCungCap.Create(i),
                rNhaCungCapList = DDrNhaCungCap.rNhaCungCaps
            };
        }
    }
}
