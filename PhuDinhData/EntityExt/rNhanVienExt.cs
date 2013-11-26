using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rNhanVien
    {
        public List<rPhuongTien> rPhuongTienList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [PhuongTien {1}] [TenNhanVien {2}]", Ma, rPhuongTien, TenNhanVien);
        }
    }
}
