using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChiPhi
    {
        public List<rNhanVien> rNhanVienList { get; set; }
        public List<rLoaiChiPhi> rLoaiChiPhiList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [NhanVien {2}] [SoTien {3}] [GhiChu {4}]", Ma, Ngay
                , rNhanVien == null ? "" : rNhanVien.TenNhanVien
                , SoTien, GhiChu);
        }
    }
}
