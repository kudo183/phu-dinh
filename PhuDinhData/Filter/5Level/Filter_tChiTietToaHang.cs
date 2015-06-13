namespace PhuDinhData.Filter
{
    public class Filter_tChiTietToaHang : FilterBase<tChiTietToaHang>
    {
        public const string MaToaHang = "MaToaHang";
        public const string TenToaHang = "TenToaHang";
        public const string MaChiTietDonHang = "MaChiTietDonHang";
        public const string TenChiTietDonHang = "TenChiTietDonHang";

        public Filter_tChiTietToaHang()
        {
            _filters[MaToaHang] = (p => true);
            _filters[TenToaHang] = (p => true);
            _filters[MaChiTietDonHang] = (p => true);
            _filters[TenChiTietDonHang] = (p => true);
            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaToaHang:
                    SetFilterMaChuyenHangDonHang(value as int?, setFalse);
                    break;
                case TenToaHang:
                    SetFilterTenToaHang(value as string, setFalse);
                    break;
                case MaChiTietDonHang:
                    SetFilterMaChiTietDonHang(value as int?, setFalse);
                    break;
                case TenChiTietDonHang:
                    SetFilterTenChiTietDonHang(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaChuyenHangDonHang(int? maToaHang, bool setFalse = false)
        {
            _filters[MaToaHang] = FilterNullable(maToaHang, setFalse, p => p.MaToaHang == maToaHang);

            UpdateMainFilter();
        }

        private void SetFilterTenToaHang(string tenChuyenHangDonHang, bool setFalse = false)
        {
            _filters[TenToaHang] =
                FilterText(tenChuyenHangDonHang, setFalse, "tToaHang.TenToaHang");

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
