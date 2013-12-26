namespace PhuDinhData.Filter
{
    public class Filter_rNuoc : FilterBase<rNuoc>
    {
        public const string TenNuoc = "TenNuoc";

        public Filter_rNuoc()
        {
            IsClearAllData = false;

            _filters[TenNuoc] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case TenNuoc:
                    SetFilterTenNuoc(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterTenNuoc(string tenNuoc, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenNuoc] = FilterText(tenNuoc, setFalse, p => p.TenNuoc.Contains(tenNuoc));

            UpdateMainFilter();
        }
    }
}
