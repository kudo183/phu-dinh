using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tNhapHang
    {
        public string TenNhapHang
        {
            get
            {
                return string.Format("{0}_{1}",
                    Ngay.ToShortDateString(), rNhaCungCap.TenNhaCungCap);
            }
        }

        public List<rNhanVien> rNhanVienList { get; set; }
        public List<rNhaCungCap> rNhaCungCapList { get; set; }
        public List<rKhoHang> rKhoHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [NhanVien {2}] [NhaCungCap {3}] [KhoHang {4}]", Ma, Ngay, rNhanVien, rNhaCungCap, rKhoHang);
        }
    }
}
