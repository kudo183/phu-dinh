namespace PhuDinhData.Filter
{
    public class Filter_rNhaCungCap : FilterBase<rNhaCungCap>
    {
        public const string TenNhaCungCap = "TenNhaCungCap";

        public Filter_rNhaCungCap()
        {
            _filters[TenNhaCungCap] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case TenNhaCungCap:
                    SetFilterTenNhaCungCap(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterTenNhaCungCap(string tenNhaCungCap, bool setFalse = false)
        {
            _filters[TenNhaCungCap] = FilterText(tenNhaCungCap, setFalse, p => p.TenNhaCungCap.Contains(tenNhaCungCap));

            UpdateMainFilter();
        }
    }
}
