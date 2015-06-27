using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_tChiTietChuyenHangDonHang : FilterBase<tChiTietChuyenHangDonHang>
    {
        public const string MaChuyenHangDonHang = "MaChuyenHangDonHang";
        public const string TenChuyenHangDonHang = "TenChuyenHangDonHang";
        public const string MaChiTietDonHang = "MaChiTietDonHang";
        public const string TenChiTietDonHang = "TenChiTietDonHang";

        public Filter_tChiTietChuyenHangDonHang()
        {
            _filters[MaChuyenHangDonHang] = (p => true);
            _filters[TenChuyenHangDonHang] = (p => true);
            _filters[MaChiTietDonHang] = (p => true);
            _filters[TenChiTietDonHang] = (p => true);
            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaChuyenHangDonHang:
                    SetFilterMaChuyenHangDonHang(value as int?, setFalse);
                    break;
                case TenChuyenHangDonHang:
                    SetFilterTenChuyenHangDonHang(value as string, setFalse);
                    break;
                case MaChiTietDonHang:
                    SetFilterMaChiTietDonHang(value as int?, setFalse);
                    break;
                case TenChiTietDonHang:
                    SetFilterTenChiTietDonHang(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaChuyenHangDonHang(int? maChuyenHangDonHang, bool setFalse = false)
        {
            _filters[MaChuyenHangDonHang] = FilterNullable(maChuyenHangDonHang, setFalse, p => p.MaChuyenHangDonHang == maChuyenHangDonHang);

            UpdateMainFilter();
        }

        private void SetFilterTenChuyenHangDonHang(string tenChuyenHangDonHang, bool setFalse = false)
        {
            _filters[TenChuyenHangDonHang] =
                FilterText(tenChuyenHangDonHang, setFalse, "tChuyenHangDonHang.TenChuyenHangDonHang");

            UpdateMainFilter();
        }

        private void SetFilterMaChiTietDonHang(int? maChiTietDonHang, bool setFalse = false)
        {
            _filters[MaChiTietDonHang] = FilterNullable(maChiTietDonHang, setFalse, p => p.MaChiTietDonHang == maChiTietDonHang);

            UpdateMainFilter();
        }

        private void SetFilterTenChiTietDonHang(string tenChiTietDonHang, bool setFalse = false)
        {
            _filters[TenChiTietDonHang] =
                FilterText(tenChiTietDonHang, setFalse, "tChiTietDonHang.TenChiTietDonHang");

            UpdateMainFilter();
        }

    }
}
