using System.Collections.Generic;
using System.Linq;

namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("tToaHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("TenToaHang", "ThanhTien")]
    public partial class tToaHang
    {
        public string TenToaHang
        {
            get
            {
                return string.Format("{0}_{1}",
                    Ngay.ToString("dd/MM/yy")
                    , rKhachHang == null ? "" : rKhachHang.TenKhachHang);
            }
        }

        public int ThanhTien
        {
            get
            {
                return tChiTietToaHangs.Sum(p => p.GiaTien * p.tChiTietDonHang.SoLuong);
            }
        }

        public List<rKhachHang> rKhachHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [KhachHang {2}]", Ma, Ngay
                , rKhachHang == null ? "" : rKhachHang.TenKhachHang);
        }
    }
}
