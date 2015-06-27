using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_tChiTietChuyenKho : FilterBase<tChiTietChuyenKho>
    {
        public const string MaMatHang = "MaMatHang";
        public const string TenMatHang = "TenMatHang";
        public const string MaChuyenKho = "MaChuyenKho";
        public const string TenChuyenKho = "TenChuyenKho";

        public Filter_tChiTietChuyenKho()
        {
            _filters[MaMatHang] = (p => true);
            _filters[TenMatHang] = (p => true);
            _filters[MaChuyenKho] = (p => true);
            _filters[TenChuyenKho] = (p => true);
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
                case MaChuyenKho:
                    SetFilterMaChuyenKho(value as int?, setFalse);
                    break;
                case TenChuyenKho:
                    SetFilterTenChuyenKho(value as string, setFalse);
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

        private void SetFilterMaChuyenKho(int? maChuyenKho, bool setFalse = false)
        {
            _filters[MaChuyenKho] = FilterNullable(maChuyenKho, setFalse, p => p.MaChuyenKho == maChuyenKho);

            UpdateMainFilter();
        }

        private void SetFilterTenChuyenKho(string tenChuyenKho, bool setFalse = false)
        {
            _filters[TenChuyenKho] =
                FilterText(tenChuyenKho, setFalse, "tChuyenKho.TenChuyenKho");

            UpdateMainFilter();
        }
    }
}
