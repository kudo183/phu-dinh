using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rNguyenLieu
    {
        public string TenNguyenLieu
        {
            get
            {
                return string.Format("{0} {1}", rLoaiNguyenLieu.TenLoai, DuongKinh);
            }
        }

        public List<rLoaiNguyenLieu> rLoaiNguyenLieuList { get; set; }
    }
}
