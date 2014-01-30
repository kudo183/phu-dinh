using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tTonKho
    {
        public List<tMatHang> tMatHangList { get; set; }
        public List<rKhoHang> rKhoHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [MatHang {2}] [KhoHang {3}] [SoLuong {4}]", Ma, Ngay, tMatHang, rKhoHang, SoLuong);
        }
    }
}
