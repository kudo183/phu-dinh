using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rCanhBaoTonKho
    {
        public List<tMatHang> tMatHangList { get; set; }
        public List<rKhoHang> rKhoHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [MatHang {1}] [KhoHang {2}] [TonThapNhat {3}] [TonCaoNhat {4}]", Ma, tMatHang.TenMatHang, rKhoHang.TenKho, TonThapNhat, TonCaoNhat);
        }
    }
}
