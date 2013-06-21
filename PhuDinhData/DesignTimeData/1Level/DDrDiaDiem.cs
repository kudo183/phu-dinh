using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrDiaDiem
    {
        private static List<rDiaDiem> _rDiaDiems;
        public static List<rDiaDiem> rDiaDiems
        {
            get
            {
                if (_rDiaDiems != null)
                {
                    return _rDiaDiems;
                }

                const int count = 10;
                _rDiaDiems = new List<rDiaDiem>(count);
                for (var i = 1; i <= count; i++)
                {
                    _rDiaDiems.Add(Create(i));
                }
                return _rDiaDiems;
            }
        }

        public static rDiaDiem Create(int i)
        {
            return new rDiaDiem()
            {
                Ma = i,
                MaNuoc = i,
                Tinh = "Tỉnh " + i,
                rNuoc = DDrNuoc.Create(i),
                rNuocList = DDrNuoc.rNuocs
            };
        }
    }
}
