﻿namespace PhuDinhData.Filter
{
    public class Filter_rNhanVien : FilterBase<rNhanVien>
    {
        public const string MaPhuongTien = "MaPhuongTien";
        public const string TenPhuongTien = "TenPhuongTien";

        public Filter_rNhanVien()
        {
            IsClearAllData = false;

            _filters[MaPhuongTien] = (p => true);
            _filters[TenPhuongTien] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaPhuongTien:
                    SetFilterMaPhuongTien(value as int?, setFalse);
                    break;
                case TenPhuongTien:
                    SetFilterTenPhuongTien(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaPhuongTien(int? maPhuongTien, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[MaPhuongTien] = FilterNullable(maPhuongTien, setFalse, p => p.MaPhuongTien == maPhuongTien);

            UpdateMainFilter();
        }

        private void SetFilterTenPhuongTien(string tenPhuongTien, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenPhuongTien] = FilterText(tenPhuongTien, setFalse, p => p.rPhuongTien.TenPhuongTien.Contains(tenPhuongTien));

            UpdateMainFilter();
        }
    }
}