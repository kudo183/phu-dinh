using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rNhanVienGiaoHang
    {
        public rPhuongTien PhuongTien { get; set; }

        private static List<rPhuongTien> _rPhuongTiens = new List<rPhuongTien>();
        public static List<rPhuongTien> rPhuongTiens
        {
            get { return _rPhuongTiens; }
            set
            {
                _rPhuongTiens.Clear();
                _rPhuongTiens.AddRange(value);
            }
        }
    }
}
