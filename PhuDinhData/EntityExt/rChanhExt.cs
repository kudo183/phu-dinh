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

        public List<rBaiXe> rBaiXeList { get; set; }
    }
}
