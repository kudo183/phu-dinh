using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public class DDrKhachHangChanh
    {
        private static List<rKhachHangChanh> _rKhachHangChanhs;
        public static List<rKhachHangChanh> rKhachHangChanhs
        {
            get
            {
                if (_rKhachHangChanhs != null)
                {
                    return _rKhachHangChanhs;
                }

                const int count = 10;
                _rKhachHangChanhs = new List<rKhachHangChanh>(count);
                for (int i = 1; i <= count; i++)
                {
                    _rKhachHangChanhs.Add(Create(i));

                }

                return _rKhachHangChanhs;
            }
        }

        public static rKhachHangChanh Create(int i)
        {
            return new rKhachHangChanh()
            {
                Ma = i,
                MaChanh = i,
                MaKhachHang = i,
                LaMacDinh = i % 2 == 0,
                rKhachHang = DDrKhachHang.Create(i),
                rChanh = DDrChanh.Create(i),
                rKhachHangList = DDrKhachHang.rKhachHangs,
                rChanhList = DDrChanh.rChanhs
            };
        }
    }
}
