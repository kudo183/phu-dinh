namespace PhuDinhData.Filter
{
    public class Filter_rDiaDiem : FilterBase<rDiaDiem>
    {
        public const string MaNuoc = "MaNuoc";
        public const string TenNuoc = "TenNuoc";
        public const string TenTinh = "TenTinh";

        public Filter_rDiaDiem()
        {
            IsClearAllData = false;

            _filters[MaNuoc] = (p => true);
            _filters[TenNuoc] = (p => true);
            _filters[TenTinh] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaNuoc:
                    SetFilterMaNuoc(value as int?, setFalse);
                    break;
                case TenNuoc:
                    SetFilterTenNuoc(value as string, setFalse);
                    break;
                case TenTinh:
                    SetFilterTenTinh(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaNuoc(int? maNuoc, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[MaNuoc] = FilterNullable(maNuoc, setFalse, p => p.MaNuoc == maNuoc);

            UpdateMainFilter();
        }

        private void SetFilterTenNuoc(string tenNuoc, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenNuoc] = FilterText(tenNuoc, setFalse, p => p.rNuoc.TenNuoc.Contains(tenNuoc));

            UpdateMainFilter();
        }

        private void SetFilterTenTinh(string tenTinh, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenTinh] = FilterText(tenTinh, setFalse, p => p.Tinh.Contains(tenTinh));

            UpdateMainFilter();
        }
    }
}
