using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tNhapMatHang
    {
        public List<rNhanVien> rNhanVienList { get; set; }
        public List<tMatHang> tMatHangList { get; set; }
        public List<rKhoHang> rKhoHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [Ngay {1}] [NhanVien {2}] [MatHang {3}] [SoLuong {4}]", Ma, Ngay, rNhanVien, tMatHang, SoLuong);
        }
    }
}
