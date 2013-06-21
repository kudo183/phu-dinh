using System.Collections.Generic;

namespace PhuDinhData.DesignTimeData
{
    public class DDtDonHang
    {
        private static List<tDonHang> _tDonHangs;
        public static List<tDonHang> tDonHangs
        {
            get
            {
                if (_tDonHangs != null)
                {
                    return _tDonHangs;
                }

                const int count = 10;
                _tDonHangs = new List<tDonHang>(count);
                for (int i = 1; i <= count; i++)
                {
                    _tDonHangs.Add(Create(i));

                }

                return _tDonHangs;
            }
        }

        public static tDonHang Create(int i)
        {
            return new tDonHang()
            {
                Ma = i,
                MaChanh = i,
                MaKhachHang = i,
                Ngay = System.DateTime.Now,
                rKhachHang = DDrKhachHang.Create(i),
                rChanh = DDrChanh.Create(i),
                rKhachHangList = DDrKhachHang.rKhachHangs,
                rChanhList = DDrChanh.rChanhs
            };
        }
    }
}
