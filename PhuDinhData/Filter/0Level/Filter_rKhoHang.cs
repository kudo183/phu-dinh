using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_rKhoHang : FilterBase<rKhoHang>
    {
        public const string TenKho = "TenKho";

        public Filter_rKhoHang()
        {
            _filters[TenKho] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case TenKho:
                    SetFilterDiaDiemBaiXe(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterDiaDiemBaiXe(string tenKho, bool setFalse = false)
        {
            _filters[TenKho] = FilterText(tenKho, setFalse, "TenKho");

            UpdateMainFilter();
        }
    }
}
