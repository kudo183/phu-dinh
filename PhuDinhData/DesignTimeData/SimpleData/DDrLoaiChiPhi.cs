using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrLoaiChiPhi
    {
        private static List<rLoaiChiPhi> _rLoaiChiPhis;
        public static List<rLoaiChiPhi> rLoaiChiPhis
        {
            get
            {
                if (_rLoaiChiPhis != null)
                {
                    return _rLoaiChiPhis;
                }

                const int count = 10;
                _rLoaiChiPhis = new List<rLoaiChiPhi>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rLoaiChiPhis.Add(Create(i));
                }

                return _rLoaiChiPhis;
            }
        }

        public static rLoaiChiPhi Create(int i)
        {
            return new rLoaiChiPhi()
            {
                Ma = i,
                TenLoaiChiPhi = "Loại chi phí " + i
            };
        }
    }
}
