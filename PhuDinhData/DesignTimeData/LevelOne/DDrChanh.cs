using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrChanh
    {
        private static List<rChanh> _rChanhs;
        public static List<rChanh> rChanhs
        {
            get
            {
                if (_rChanhs != null)
                {
                    return _rChanhs;
                }

                const int count = 10;
                _rChanhs = new List<rChanh>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rChanhs.Add(Create(i));
                }
                return _rChanhs;
            }
        }

        public static rChanh Create(int i)
        {
            return new rChanh()
            {
                Ma = i,
                MaBaiXe = i,
                TenChanh = "Chành " + i,
                rBaiXe = DDrBaiXe.Create(i),
                rBaiXeList = DDrBaiXe.rBaiXes
            };
        }
    }
}
