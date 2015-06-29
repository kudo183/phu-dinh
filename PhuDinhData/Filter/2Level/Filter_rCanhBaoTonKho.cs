using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_rCanhBaoTonKho : FilterBase<rCanhBaoTonKho>
    {
        public const string MaMatHang = "MaMatHang";
        public const string TenMatHang = "TenMatHang";
        public const string MaKhoHang = "MaKhoHang";
        public const string TenKhoHang = "TenKhoHang";

        public Filter_rCanhBaoTonKho()
        {
            _filters[MaMatHang] = (p => true);
            _filters[TenMatHang] = (p => true);
            _filters[MaKhoHang] = (p => true);
            _filters[TenKhoHang] = (p => true);

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
                case MaKhoHang:
                    SetFilterMaKhoHang(value as int?, setFalse);
                    break;
                case TenKhoHang:
                    SetFilterTenKhoHang(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaMatHang(int? maMatHang, bool setFalse = false)
        {
            _filters[MaMatHang] = FilterNullable(maMatHang, setFalse, "MaMatHang");

            UpdateMainFilter();
        }

        private void SetFilterTenMatHang(string tenMatHang, bool setFalse = false)
        {
            _filters[TenMatHang] = FilterText(tenMatHang, setFalse, "tMatHang.TenMatHang");

            UpdateMainFilter();
        }

        private void SetFilterMaKhoHang(int? maKhoHang, bool setFalse = false)
        {
            _filters[MaKhoHang] = FilterNullable(maKhoHang, setFalse, "MaKhoHang");

            UpdateMainFilter();
        }

        private void SetFilterTenKhoHang(string tenKhoHang, bool setFalse = false)
        {
            _filters[TenKhoHang] = FilterText(tenKhoHang, setFalse, "rKhoHang.TenKho");

            UpdateMainFilter();
        }
    }
}
