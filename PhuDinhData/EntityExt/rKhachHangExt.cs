using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rKhachHang
    {
        public string TenKhachHangDiaDiem
        {
            get { return string.Format("{0}_{1}", TenKhachHang, rDiaDiem.TenDiaDiem); }
        }

        public rDiaDiem DiaDiem { get; set; }

        private static List<rDiaDiem> _rDiaDiem = new List<rDiaDiem>();
        public static List<rDiaDiem> rDiaDiems
        {
            get { return _rDiaDiem; }
            set { _rDiaDiem = value; }
        }
    }
}
