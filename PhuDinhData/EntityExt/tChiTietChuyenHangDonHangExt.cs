using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData
{
    public partial class tChiTietChuyenHangDonHang
    {
        public int SoLuongTheoDonHang
        {
            get
            {
                if (tChiTietDonHang != null)
                {
                    return tChiTietDonHang.SoLuong;
                }

                return 0;
            }
        }

        public List<tChuyenHangDonHang> tChuyenHangDonHangList { get; set; }

        private List<tChiTietDonHang> _tChiTietDonHangList;
        public List<tChiTietDonHang> tChiTietDonHangList
        {
            get { return _tChiTietDonHangList; }
            set
            {
                var mas = value.Select(p => p.Ma).ToList();

                if (tChiTietDonHang != null
                    && mas.Contains(tChiTietDonHang.Ma) == false)
                {
                    _tChiTietDonHangList = new List<tChiTietDonHang>(value);
                    _tChiTietDonHangList.Insert(0, tChiTietDonHang);
                }
                else
                {
                    _tChiTietDonHangList = value;
                }
            }
        }
    }
}
