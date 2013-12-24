namespace PhuDinhData.Filter
{
    public class Filter_rNguyenLieu : FilterBase<rNguyenLieu>
    {
        public const string MaLoaiNguyenLieu = "MaLoaiNguyenLieu";
        public const string TenLoaiNguyenLieu = "TenLoaiNguyenLieu";

        public Filter_rNguyenLieu()
        {
            IsClearAllData = false;

            _filters[MaLoaiNguyenLieu] = (p => true);
            _filters[TenLoaiNguyenLieu] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaLoaiNguyenLieu:
                    SetFilterMaLoaiNguyenLieu(value as int?, setFalse);
                    break;
                case TenLoaiNguyenLieu:
                    SetFilterTenLoaiNguyenLieu(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaLoaiNguyenLieu(int? maLoaiNguyenLieu, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[MaLoaiNguyenLieu] = FilterNullable(maLoaiNguyenLieu, setFalse, p => p.MaLoaiNguyenLieu == maLoaiNguyenLieu);

            UpdateMainFilter();
        }

        private void SetFilterTenLoaiNguyenLieu(string tenLoaiNguyenLieu, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenLoaiNguyenLieu] = FilterText(tenLoaiNguyenLieu, setFalse, p => p.rLoaiNguyenLieu.TenLoai.Contains(tenLoaiNguyenLieu));

            UpdateMainFilter();
        }
    }
}
