using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tChuyenKho
    {
        public string TenChuyenKho
        {
            get
            {
                return string.Format("{0}_{1}_{2}",
                    Ngay.ToShortDateString(), rKhoHangXuat.TenKho, rKhoHangNhap.TenKho);
            }
        }

        public List<rNhanVien> rNhanVienList { get; set; }
        public List<rKhoHang> rKhoHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [NhanVien {2}] [KhoHangXuat {3}] [KhoHangNhap {4}]", Ma, Ngay, rNhanVien, rKhoHangXuat, rKhoHangNhap);
        }
    }
}
