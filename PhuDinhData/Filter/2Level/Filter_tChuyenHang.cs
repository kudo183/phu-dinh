﻿using System;

namespace PhuDinhData.Filter
{
    public class Filter_tChuyenHang : FilterBase<tChuyenHang>
    {
        public const string MaNhanVien = "MaNhanVien";
        public const string TenNhanVien = "TenNhanVien";
        public const string Ngay = "Ngay";

        public Filter_tChuyenHang()
        {
            IsClearAllData = false;
;
            _filters[MaNhanVien] = (p => true);
            _filters[TenNhanVien] = (p => true);
            _filters[Ngay] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaNhanVien:
                    SetFilterMaNhanVien(value as int?, setFalse);
                    break;
                case TenNhanVien:
                    SetFilterTenNhanVien(value as string, setFalse);
                    break;
                case Ngay:
                    SetFilterNgay(value as DateTime?, setFalse);
                    break;
            }
        }

        private void SetFilterMaNhanVien(int? maNhanVien, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[MaNhanVien] = FilterNullable(maNhanVien, setFalse, p => p.MaNhanVienGiaoHang == maNhanVien);

            UpdateMainFilter();
        }

        private void SetFilterTenNhanVien(string tenNhanVien, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenNhanVien] = FilterText(tenNhanVien, setFalse, p => p.rNhanVien.TenNhanVien.Contains(tenNhanVien));

            UpdateMainFilter();
        }

        private void SetFilterNgay(DateTime? date, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[Ngay] = FilterNullable(date, setFalse, p => p.Ngay == date);

            UpdateMainFilter();
        }
    }
}