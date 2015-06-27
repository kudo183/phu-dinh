using System.Collections.Generic;
using System.Linq;

namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("tChuyenKhos")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("TenChuyenKho", "TongSoLuong")]
    public partial class tChuyenKho
    {
        public string TenChuyenKho
        {
            get
            {
                return string.Format("{0}_{1}_{2}",
                    Ngay.ToShortDateString()
                    , rKhoHangXuat == null ? "" : rKhoHangXuat.TenKho
                    , rKhoHangNhap == null ? "" : rKhoHangNhap.TenKho);
            }
        }

        public int TongSoLuong
        {
            get
            {
                int result = 0;
                if (tChiTietChuyenKhoes != null)
                {
                    result += tChiTietChuyenKhoes.Sum(tChiTietChuyenKho => tChiTietChuyenKho.SoLuong);
                }
                return result;
            }
        }

        public List<rNhanVien> rNhanVienList { get; set; }
        public List<rKhoHang> rKhoHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [NhanVien {2}] [KhoHangXuat {3}] [KhoHangNhap {4}]", Ma, Ngay
                , rNhanVien == null ? "" : rNhanVien.TenNhanVien
                , rKhoHangXuat == null ? "" : rKhoHangXuat.TenKho
                , rKhoHangNhap == null ? "" : rKhoHangNhap.TenKho);
        }
    }
}
