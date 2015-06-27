using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_tChuyenHangDonHang : FilterBase<tChuyenHangDonHang>
    {
        public const string MaChuyenHang = "MaChuyenHang";
        public const string TenChuyenHang = "TenChuyenHang";
        public const string MaDonHang = "MaDonHang";
        public const string TenDonHang = "TenDonHang";

        public Filter_tChuyenHangDonHang()
        {
            _filters[MaChuyenHang] = (p => true);
            _filters[TenChuyenHang] = (p => true);
            _filters[MaDonHang] = (p => true);
            _filters[TenDonHang] = (p => true);
            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaChuyenHang:
                    SetFilterMaChuyenHang(value as int?, setFalse);
                    break;
                case TenChuyenHang:
                    SetFilterTenChuyenHang(value as string, setFalse);
                    break;
                case MaDonHang:
                    SetFilterMaDonHang(value as int?, setFalse);
                    break;
                case TenDonHang:
                    SetFilterTenDonHang(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaChuyenHang(int? maChuyenHang, bool setFalse = false)
        {
            _filters[MaChuyenHang] = FilterNullable(maChuyenHang, setFalse, p => p.MaChuyenHang == maChuyenHang);

            UpdateMainFilter();
        }

        private void SetFilterTenChuyenHang(string tenChuyenHang, bool setFalse = false)
        {
            _filters[TenChuyenHang] =
                FilterText(tenChuyenHang, setFalse, "tChuyenHang.TenChuyenHang");

            UpdateMainFilter();
        }

        private void SetFilterMaDonHang(int? maDonHang, bool setFalse = false)
        {
            _filters[MaDonHang] = FilterNullable(maDonHang, setFalse, p => p.MaDonHang == maDonHang);

            UpdateMainFilter();
        }

        private void SetFilterTenDonHang(string tenDonHang, bool setFalse = false)
        {
            _filters[TenDonHang] =
                FilterText(tenDonHang, setFalse, "tDonHang.TenDonHang");

            UpdateMainFilter();
        }
    }
}
