using System.Collections.Generic;

namespace PhuDinhData
{
    [global::System.Data.Services.Common.EntitySetAttribute("rCanhBaoTonKhos")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("")]
    public partial class rCanhBaoTonKho
    {
        public List<tMatHang> tMatHangList { get; set; }
        public List<rKhoHang> rKhoHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [MatHang {1}] [KhoHang {2}] [TonThapNhat {3}] [TonCaoNhat {4}]", Ma
                , tMatHang == null ? "" : tMatHang.TenMatHang
                , rKhoHang == null ? "" : rKhoHang.TenKho
                , TonThapNhat, TonCaoNhat);
        }
    }
}
