using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class tMatHang
    {
        public rLoaiHang LoaiHang { get; set; }

        private static List<rLoaiHang> _rLoaiHangs = new List<rLoaiHang>();
        public static List<rLoaiHang> rLoaiHangs
        {
            get { return _rLoaiHangs; }
            set { _rLoaiHangs = value; }
        }
    }
}
