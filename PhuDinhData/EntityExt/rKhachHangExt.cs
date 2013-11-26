using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rKhachHang
    {
        public string TenKhachHangDiaDiem
        {
            get { return string.Format("{0}", TenKhachHang); }
        }

        public List<rDiaDiem> rDiaDiemList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [TenKhachHang {1}] [DiaDiem {2}]", Ma, TenKhachHang, rDiaDiem);
        }
    }
}
