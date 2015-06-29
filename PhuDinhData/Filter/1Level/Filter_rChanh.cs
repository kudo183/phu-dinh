using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_rChanh : FilterBase<rChanh>
    {
        public const string MaBaiXe = "MaBaiXe";
        public const string DiaDiemBaiXe = "DiaDiemBaiXe";
        public const string TenChanh = "TenChanh";

        public Filter_rChanh()
        {
            _filters[MaBaiXe] = (p => true);
            _filters[DiaDiemBaiXe] = (p => true);
            _filters[TenChanh] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaBaiXe:
                    SetFilterMaBaiXe(value as int?, setFalse);
                    break;
                case DiaDiemBaiXe:
                    SetFilterDiaDiemBaiXe(value as string, setFalse);
                    break;
                case TenChanh:
                    SetFilterTenChanh(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaBaiXe(int? maBaiXe, bool setFalse = false)
        {
            _filters[MaBaiXe] = FilterNullable(maBaiXe, setFalse, "MaBaiXe");

            UpdateMainFilter();
        }

        private void SetFilterDiaDiemBaiXe(string diaDiemBaiXe, bool setFalse = false)
        {
            _filters[DiaDiemBaiXe] = FilterText(diaDiemBaiXe, setFalse, "rBaiXe.DiaDiemBaiXe");

            UpdateMainFilter();
        }

        private void SetFilterTenChanh(string tenChanh, bool setFalse = false)
        {
            _filters[TenChanh] = FilterText(tenChanh, setFalse, "TenChanh");

            UpdateMainFilter();
        }
    }
}
