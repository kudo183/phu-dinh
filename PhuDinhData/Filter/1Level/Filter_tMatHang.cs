using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_tMatHang : FilterBase<tMatHang>
    {
        public const string MaLoaiHang = "MaLoaiHang";
        public const string TenLoaiHang = "TenLoaiHang";

        public Filter_tMatHang()
        {
            _filters[MaLoaiHang] = (p => true);
            _filters[TenLoaiHang] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaLoaiHang:
                    SetFilterMaLoaiHang(value as int?, setFalse);
                    break;
                case TenLoaiHang:
                    SetFilterTenLoaiHang(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaLoaiHang(int? maLoai, bool setFalse = false)
        {
            _filters[MaLoaiHang] = FilterNullable(maLoai, setFalse, "MaLoai");

            UpdateMainFilter();
        }

        private void SetFilterTenLoaiHang(string tenLoaiHang, bool setFalse = false)
        {
            _filters[TenLoaiHang] = FilterText(tenLoaiHang, setFalse, "rLoaiHang.TenLoai");

            UpdateMainFilter();
        }
    }
}
