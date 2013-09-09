using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiPhiNhanVienGiaoHang
    {
        private static List<tChiPhiNhanVienGiaoHang> _tChiPhiNhanVienGiaoHangs;
        public static List<tChiPhiNhanVienGiaoHang> tChiPhiNhanVienGiaoHangs
        {
            get
            {
                if (_tChiPhiNhanVienGiaoHangs != null)
                {
                    return _tChiPhiNhanVienGiaoHangs;
                }

                const int count = 10;
                _tChiPhiNhanVienGiaoHangs = new List<tChiPhiNhanVienGiaoHang>(count);
                for (var i = 1; i <= count; i++)
                {
                    _tChiPhiNhanVienGiaoHangs.Add(Create(i));
                }

                return _tChiPhiNhanVienGiaoHangs;
            }
        }

        public static tChiPhiNhanVienGiaoHang Create(int i)
        {
            return new tChiPhiNhanVienGiaoHang()
            {
                Ma = i,
                MaLoaiChiPhi = i,
                MaNhanVienGiaoHang = i,
                SoTien = i * 1000,
                GhiChu = "Ghi chú " + i,
                rNhanVien = DDrNhanVien.Create(i),
                rLoaiChiPhi = DDrLoaiChiPhi.Create(i),
                rNhanVienList = DDrNhanVien.rNhanViens,
                rLoaiChiPhiList = DDrLoaiChiPhi.rLoaiChiPhis
            };
        }
    }
}
