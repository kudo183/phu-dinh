using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNhaCungCap
    {
        private static List<rNhaCungCap> _rNhaCungCaps;
        public static List<rNhaCungCap> rNhaCungCaps
        {
            get
            {
                if (_rNhaCungCaps != null)
                {
                    return _rNhaCungCaps;
                }

                const int count = 10;
                _rNhaCungCaps = new List<rNhaCungCap>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rNhaCungCaps.Add(Create(i));
                }
                return _rNhaCungCaps;
            }
        }

        public static rNhaCungCap Create(int i)
        {
            return new rNhaCungCap()
            {
                Ma = i,
                TenNhaCungCap = "Nha cung cap " + i
            };
        }
    }
}
