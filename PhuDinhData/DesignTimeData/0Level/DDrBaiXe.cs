using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrBaiXe
    {
        private static List<rBaiXe> _rBaiXes;
        public static List<rBaiXe> rBaiXes
        {
            get
            {
                if (_rBaiXes != null)
                {
                    return _rBaiXes;
                }

                const int count = 10;
                _rBaiXes = new List<rBaiXe>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rBaiXes.Add(Create(i));
                }
                return _rBaiXes;
            }
        }

        public static rBaiXe Create(int i)
        {
            return new rBaiXe()
            {
                Ma = i,
                DiaDiemBaiXe = "Bai xe " + i
            };
        }
    }
}
