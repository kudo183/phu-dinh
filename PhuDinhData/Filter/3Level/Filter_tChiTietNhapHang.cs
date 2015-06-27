using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_tChiTietNhapHang : FilterBase<tChiTietNhapHang>
    {
        public const string MaMatHang = "MaMatHang";
        public const string TenMatHang = "TenMatHang";
        public const string MaNhapHang = "MaNhapHang";
        public const string TenNhapHang = "TenNhapHang";

        public Filter_tChiTietNhapHang()
        {
            _filters[MaMatHang] = (p => true);
            _filters[TenMatHang] = (p => true);
            _filters[MaNhapHang] = (p => true);
            _filters[TenNhapHang] = (p => true);
            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaMatHang:
                    SetFilterMaMatHang(value as int?, setFalse);
                    break;
                case TenMatHang:
                    SetFilterTenMatHang(value as string, setFalse);
                    break;
                case MaNhapHang:
                    SetFilterMaNhapHang(value as int?, setFalse);
                    break;
                case TenNhapHang:
                    SetFilterTenNhapHang(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaMatHang(int? maMatHang, bool setFalse = false)
        {
            _filters[MaMatHang] = FilterNullable(maMatHang, setFalse, p => p.MaMatHang == maMatHang);

            UpdateMainFilter();
        }

        private void SetFilterTenMatHang(string tenMatHang, bool setFalse = false)
        {
            _filters[TenMatHang] =
                FilterText(tenMatHang, setFalse, "tMatHang.TenMatHang");

            UpdateMainFilter();
        }

        private void SetFilterMaNhapHang(int? maNhapHang, bool setFalse = false)
        {
            _filters[MaNhapHang] = FilterNullable(maNhapHang, setFalse, p => p.MaNhapHang == maNhapHang);

            UpdateMainFilter();
        }

        private void SetFilterTenNhapHang(string tenNhapHang, bool setFalse = false)
        {
            _filters[TenNhapHang] =
                FilterText(tenNhapHang, setFalse, "tNhapHang.TenNhapHang");

            UpdateMainFilter();
        }
    }
}
