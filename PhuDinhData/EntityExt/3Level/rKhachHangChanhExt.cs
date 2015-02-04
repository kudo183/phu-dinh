using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rKhachHangChanh
    {
        public List<rKhachHang> rKhachHangList { get; set; }
        public List<rChanh> rChanhList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [KhachHang {1}] [Chanh {2}]", Ma
                , rKhachHang == null ? "" : rKhachHang.TenKhachHang
                , rChanh == null ? "" : rChanh.TenChanh);
        }
    }
}
