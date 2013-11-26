using System.Collections.Generic;
using System.Linq;

namespace PhuDinhData
{
    public partial class tChuyenHangDonHang
    {
        public string TenChuyenHangDonHang
        {
            get { return string.Format("{0}_{1}", tChuyenHang.TenChuyenHang, tDonHang.TenDonHang); }
        }

        public int TongSoLuongTheoDonHang
        {
            get
            {
                int result = 0;

                if (tDonHang != null)
                {
                    result += tDonHang.tChiTietDonHangs.Sum(tChiTietDonHang => tChiTietDonHang.SoLuong);
                }

                return result;
            }
        }

        public int TongSoLuongThucTe
        {
            get
            {
                int result = 0;
                if (tChiTietChuyenHangDonHangs != null)
                {
                    result += tChiTietChuyenHangDonHangs.Sum(tChiTietChuyenHangDonHang => tChiTietChuyenHangDonHang.SoLuong);
                }
                return result;
            }
        }

        public List<tChuyenHang> tChuyenHangList { get; set; }

        private List<tDonHang> _tDonHangList;
        public List<tDonHang> tDonHangList
        {
            get { return _tDonHangList; }
            set
            {
                var mas = value.Select(p => p.Ma).ToList();

                if (tDonHang != null
                    && mas.Contains(tDonHang.Ma) == false)
                {
                    _tDonHangList = new List<tDonHang>(value);
                    _tDonHangList.Insert(0, tDonHang);
                }
                else
                {
                    _tDonHangList = value;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("[Ma {0}] [ChuyenHang {1}] [DonHang {2}]", Ma,tChuyenHang, tDonHang);
        }
    }
}
