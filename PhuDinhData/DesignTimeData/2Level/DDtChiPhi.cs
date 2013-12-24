using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiPhi
    {
        private static List<tChiPhi> _tChiPhis;
        public static List<tChiPhi> tChiPhis
        {
            get
            {
                if (_tChiPhis != null)
                {
                    return _tChiPhis;
                }

                const int count = 10;
                _tChiPhis = new List<tChiPhi>(count);
                for (var i = 1; i <= count; i++)
                {
                    _tChiPhis.Add(Create(i));
                }

                return _tChiPhis;
            }
        }

        public static tChiPhi Create(int i)
        {
            return new tChiPhi()
            {
                Ma = i,
                MaLoaiChiPhi = i,
                MaNhanVienGiaoHang = i,
                SoTien = i * 1000,
                GhiChu = "Ghi chú " + i,
                rNhanVien = DDrNhanVien.Create(i),
                rLoaiChiPhi = DDrLoaiChiPhi.Create(i),
                rNhanVienList = DDrNhanVien.ViewModel.Entity.ToList(),
                rLoaiChiPhiList = DDrLoaiChiPhi.rLoaiChiPhis
            };
        }
    }
}
