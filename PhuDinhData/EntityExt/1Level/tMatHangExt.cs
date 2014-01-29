using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tMatHang
    {
        public string TenMatHangLoaiHang
        {
            get
            {
                return string.Format("{0}", TenMatHang);
            }
        }

        public List<rLoaiHang> rLoaiHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [LoaiHang {1}] [TenMatHang {2}] [TenMatHangDayDu {3}] [SoMet {4}] [SoKy {5}]", Ma, rLoaiHang, TenMatHang, TenMatHangDayDu, SoMet, SoKy);
        }
    }
}
