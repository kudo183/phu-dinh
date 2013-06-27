using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tMatHang
    {
        public string TenMatHangLoaiHang
        {
            get
            {
                return string.Format("{0}_{1}", rLoaiHang.TenLoai, TenMatHang);
            }
        }

        public List<rLoaiHang> rLoaiHangList { get; set; }
    }
}
