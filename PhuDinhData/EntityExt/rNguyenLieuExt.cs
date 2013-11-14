using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rNguyenLieu
    {
        public string TenNguyenLieu
        {
            get
            {
                return string.Format("{0} {1}", rLoaiNguyenLieu.TenLoai, ConvertDuongKinhDayToString(DuongKinh));
            }
        }

        public List<rLoaiNguyenLieu> rLoaiNguyenLieuList { get; set; }

        private string ConvertDuongKinhDayToString(int duongKinh)
        {
            string result = string.Empty;
            if (duongKinh < 100)
            {
                int dem = duongKinh / 10;
                int le = duongKinh % 10;
                result = string.Format("{0}dem{1}", dem, (le == 0) ? "" : le.ToString());
            }
            else if (duongKinh >= 100)
            {
                int ly = duongKinh / 100;
                int le = duongKinh % 100;
                result = string.Format("{0}ly{1}", ly, (le == 0) ? "" : le.ToString());
            }
            return result;
        }
    }
}
