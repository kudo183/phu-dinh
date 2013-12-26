namespace PhuDinhData.Filter
{
    public class Filter_rLoaiChiPhi : FilterBase<rLoaiChiPhi>
    {
        public const string TenLoaiChiPhi = "TenLoaiChiPhi";

        public Filter_rLoaiChiPhi()
        {
            IsClearAllData = false;

            _filters[TenLoaiChiPhi] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case TenLoaiChiPhi:
                    SetFilterTenLoaiChiPhi(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterTenLoaiChiPhi(string tenLoaiChiPhi, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenLoaiChiPhi] = FilterText(tenLoaiChiPhi, setFalse, p => p.TenLoaiChiPhi.Contains(tenLoaiChiPhi));

            UpdateMainFilter();
        }
    }
}
