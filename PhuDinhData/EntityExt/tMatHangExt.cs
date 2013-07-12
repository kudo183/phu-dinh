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
    }
}
