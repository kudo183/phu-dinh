using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData
{
    public partial class tChiTietToaHang
    {
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

        public int ThanhTien
        {
            get { return tChiTietDonHang == null ? 0 : tChiTietDonHang.SoLuong * GiaTien; }
        }

        public List<tToaHang> tToaHangList { get; set; }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [ChiTietDonHang {1}]", Ma
                , tChiTietDonHang == null ? "" : tChiTietDonHang.TenChiTietDonHang);
        }
    }
}
