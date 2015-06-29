using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_rKhachHang : FilterBase<rKhachHang>
    {
        public const string MaDiaDiem = "MaDiaDiem";
        public const string Tinh = "Tinh";
        public const string TenKhachHang = "TenKhachHang";

        public Filter_rKhachHang()
        {
            _filters[MaDiaDiem] = (p => true);
            _filters[Tinh] = (p => true);
            _filters[TenKhachHang] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaDiaDiem:
                    SetFilterMaDiaDiem(value as int?, setFalse);
                    break;
                case Tinh:
                    SetFilterTinh(value as string, setFalse);
                    break;
                case TenKhachHang:
                    SetFilterTenKhachHang(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaDiaDiem(int? maDiaDiem, bool setFalse = false)
        {
            _filters[MaDiaDiem] = FilterNullable(maDiaDiem, setFalse, "MaDiaDiem");

            UpdateMainFilter();
        }

        private void SetFilterTinh(string tinh, bool setFalse = false)
        {
            _filters[Tinh] = FilterText(tinh, setFalse, "rDiaDiem.Tinh");

            UpdateMainFilter();
        }

        private void SetFilterTenKhachHang(string tenKhachHang, bool setFalse = false)
        {
            _filters[TenKhachHang] = FilterText(tenKhachHang, setFalse, "TenKhachHang");

            UpdateMainFilter();
        }
    }
}
