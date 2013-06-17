using System.Collections.Generic;

namespace PhuDinhData
{
    public partial class rChanh
    {
        public string TenBaiXeChanh
        {
            get { return string.Format("{0}_{1}", rBaiXe.DiaDiemBaiXe, TenChanh); }
        }

        public List<rBaiXe> rBaiXeList { get; set; }
    }
}
