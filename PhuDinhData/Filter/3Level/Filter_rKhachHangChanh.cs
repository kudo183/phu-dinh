using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_rKhachHangChanh : FilterBase<rKhachHangChanh>
    {
        public const string MaKhachHang = "MaKhachHang";
        public const string TenKhachHang = "TenKhachHang";
        public const string MaChanh = "MaChanh";
        public const string TenChanh = "TenChanh";

        public Filter_rKhachHangChanh()
        {
            _filters[MaKhachHang] = (p => true);
            _filters[TenKhachHang] = (p => true);
            _filters[MaChanh] = (p => true);
            _filters[TenChanh] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaKhachHang:
                    SetFilterMaKhachHang(value as int?, setFalse);
                    break;
                case TenKhachHang:
                    SetFilterTenKhachHang(value as string, setFalse);
                    break;
                case MaChanh:
                    SetFilterMaChanh(value as int?, setFalse);
                    break;
                case TenChanh:
                    SetFilterTenChanh(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaKhachHang(int? maKhachHang, bool setFalse = false)
        {
            _filters[MaKhachHang] = FilterNullable(maKhachHang, setFalse, "MaKhachHang");

            UpdateMainFilter();
        }

        private void SetFilterTenKhachHang(string tenKhachHang, bool setFalse = false)
        {
            _filters[TenKhachHang] = FilterText(tenKhachHang, setFalse, "rKhachHang.TenKhachHang");

            UpdateMainFilter();
        }

        private void SetFilterMaChanh(int? maChanh, bool setFalse = false)
        {
            _filters[MaChanh] = FilterNullable(maChanh, setFalse, "MaChanh");

            UpdateMainFilter();
        }

        private void SetFilterTenChanh(string tenChanh, bool setFalse = false)
        {
            _filters[TenChanh] = FilterText(tenChanh, setFalse, "rChanh.TenChanh");

            UpdateMainFilter();
        }
    }
}
