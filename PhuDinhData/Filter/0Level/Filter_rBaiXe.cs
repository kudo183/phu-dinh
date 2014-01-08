namespace PhuDinhData.Filter
{
    public class Filter_rBaiXe : FilterBase<rBaiXe>
    {
        public const string DiaDiemBaiXe = "DiaDiemBaiXe";

        public Filter_rBaiXe()
        {
            _filters[DiaDiemBaiXe] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case DiaDiemBaiXe:
                    SetFilterDiaDiemBaiXe(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterDiaDiemBaiXe(string diaDiemBaiXe, bool setFalse = false)
        {
            _filters[DiaDiemBaiXe] = FilterText(diaDiemBaiXe, setFalse, p => p.DiaDiemBaiXe.Contains(diaDiemBaiXe));

            UpdateMainFilter();
        }
    }
}
