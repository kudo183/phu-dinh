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

        public override string ToString()
        {
            return string.Format("[Ma {0}] [ChuyenHangDonHang {1}] [ChiTietDonHang {2}] [SoLuong {3}]", Ma
                , tChuyenHangDonHang == null ? "" : tChuyenHangDonHang.TenChuyenHangDonHang
                , tChiTietDonHang == null ? "" : tChiTietDonHang.TenChiTietDonHang
                , SoLuong);
        }
    }
}
