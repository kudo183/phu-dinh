using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_rLoaiChiPhi : FilterBase<rLoaiChiPhi>
    {
        public const string TenLoaiChiPhi = "TenLoaiChiPhi";

        public Filter_rLoaiChiPhi()
        {
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
            _filters[TenLoaiChiPhi] = FilterText(tenLoaiChiPhi, setFalse, "TenLoaiChiPhi");

            UpdateMainFilter();
        }
    }
}
