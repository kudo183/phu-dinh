using System.Collections.Generic;

namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("tNhapNguyenLieus")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("")]
    public partial class tNhapNguyenLieu
    {
        public List<rNguyenLieu> rNguyenLieuList { get; set; }
        public List<rNhaCungCap> rNhaCungCapList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [NguyenLieu {2}] [NhaCungCap {3}] [SoLuong {4}]", Ma, Ngay
                , rNguyenLieu == null ? "" : rNguyenLieu.TenNguyenLieu
                , rNhaCungCap == null ? "" : rNhaCungCap.TenNhaCungCap
                , SoLuong);
        }
    }
}
