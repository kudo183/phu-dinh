using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rKhachHang
    {
        public string TenKhachHangDiaDiem
        {
            get { return string.Format("{0}_{1}", TenKhachHang, rDiaDiem.TenDiaDiem); }
        }

        public List<rDiaDiem> rDiaDiemList { get; set; }
    }
}
