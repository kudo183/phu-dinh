using System.Collections.Generic;

namespace PhuDinhDataEntity
{
    [global::System.Data.Services.Common.EntitySetAttribute("tTonKhos")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Ma")]
    [global::System.Data.Services.IgnoreProperties("CanhBao")]
    public partial class tTonKho
    {
        public List<tMatHang> tMatHangList { get; set; }
        public List<rKhoHang> rKhoHangList { get; set; }

        public int CanhBao { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [MatHang {2}] [KhoHang {3}] [SoLuong {4}]", Ma, Ngay
                , tMatHang == null ? "" : tMatHang.TenMatHang
                , rKhoHang == null ? "" : rKhoHang.TenKho
                , SoLuong);
        }
    }
}
