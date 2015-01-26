using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tNhapNguyenLieu
    {
        public List<rNguyenLieu> rNguyenLieuList { get; set; }
        public List<rNhaCungCap> rNhaCungCapList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [NguyenLieu {2}] [NhaCungCap {3}] [SoLuong {4}]", Ma, Ngay, rNguyenLieu.TenNguyenLieu, rNhaCungCap.TenNhaCungCap, SoLuong);
        }
    }
}
