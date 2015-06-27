using System.Collections.Generic;
using System.Linq;

namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("tDonHangs")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("TenDonHang", "TongSoLuong")]
    public partial class tDonHang
    {
        public string TenDonHang
        {
            get
            {
                return string.Format("{0}_{1}",
                    Ngay.ToShortDateString()
                    , rKhachHang == null ? "" : rKhachHang.TenKhachHang);
            }
        }

        public int TongSoLuong
        {
            get
            {
                int result = 0;
                if (tChiTietDonHangs != null)
                {
                    result += tChiTietDonHangs.Sum(tChiTietDonHang => tChiTietDonHang.SoLuong);
                }
                return result;
            }
        }

        public List<rKhachHang> rKhachHangList { get; set; }
        public List<rChanh> rChanhList { get; set; }
        public List<rKhoHang> rKhoHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [KhachHang {2}] [Chanh {3}]", Ma, Ngay
                , rKhachHang == null ? "" : rKhachHang.TenKhachHang
                , rChanh == null ? "" : rChanh.TenChanh);
        }
    }
}
