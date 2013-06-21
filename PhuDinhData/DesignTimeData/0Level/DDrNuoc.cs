using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNuoc
    {
        public static bool IsTouched { get; set; }

        private static List<rNuoc> _rNuocs;
        public static List<rNuoc> rNuocs
        {
            get
            {
                if (_rNuocs != null)
                {
                    return _rNuocs;
                }

                const int count = 10;
                _rNuocs = new List<rNuoc>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rNuocs.Add(Create(i));
                }

                return _rNuocs;
            }
        }

        public static rNuoc Create(int i)
        {
            return new rNuoc()
            {
                Ma = i,
                TenNuoc = "Nước " + i
            };
        }
    }
}
