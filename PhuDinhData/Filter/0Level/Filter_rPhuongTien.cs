﻿using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_rPhuongTien : FilterBase<rPhuongTien>
    {
        public const string TenPhuongTien = "TenPhuongTien";

        public Filter_rPhuongTien()
        {
            _filters[TenPhuongTien] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case TenPhuongTien:
                    SetFilterTenPhuongTien(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterTenPhuongTien(string tenPhuongTien, bool setFalse = false)
        {
            _filters[TenPhuongTien] = FilterText(tenPhuongTien, setFalse, "TenPhuongTien");

            UpdateMainFilter();
        }
    }
}
