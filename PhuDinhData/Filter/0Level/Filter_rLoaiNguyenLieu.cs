﻿namespace PhuDinhData.Filter
{
    public class Filter_rLoaiNguyenLieu : FilterBase<rLoaiNguyenLieu>
    {
        public const string TenLoai = "TenLoai";

        public Filter_rLoaiNguyenLieu()
        {
            IsClearAllData = false;

            _filters[TenLoai] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case TenLoai:
                    SetFilterTenLoai(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterTenLoai(string tenLoai, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenLoai] = FilterText(tenLoai, setFalse, p => p.TenLoai.Contains(tenLoai));

            UpdateMainFilter();
        }
    }
}