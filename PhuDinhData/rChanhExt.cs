using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rChanh
    {
        public string TenBaiXeChanh
        {
            get { return string.Format("{0}_{1}", rBaiXe.DiaDiemBaiXe, TenChanh); }
        }

        public rBaiXe BaiXe { get; set; }

        private static List<rBaiXe> _rBaiXe = new List<rBaiXe>();
        public static List<rBaiXe> rBaiXes
        {
            get { return _rBaiXe; }
            set { _rBaiXe = value; }
        }
    }
}
