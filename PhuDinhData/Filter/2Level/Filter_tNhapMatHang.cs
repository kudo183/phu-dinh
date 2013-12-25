﻿using System;

namespace PhuDinhData.Filter
{
    public class Filter_tNhapMatHang : FilterBase<tNhapMatHang>
    {
        public const string MaMatHang = "MaMatHang";
        public const string TenMatHang = "TenMatHang";
        public const string MaNhanVien = "MaNhanVien";
        public const string TenNhanVien = "TenNhanVien";
        public const string Ngay = "Ngay";

        public Filter_tNhapMatHang()
        {
            IsClearAllData = false;

            _filters[MaMatHang] = (p => true);
            _filters[TenMatHang] = (p => true);
            _filters[MaNhanVien] = (p => true);
            _filters[TenNhanVien] = (p => true);
            _filters[Ngay] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaMatHang:
                    SetFilterMaMatHang(value as int?, setFalse);
                    break;
                case TenMatHang:
                    SetFilterTenMatHang(value as string, setFalse);
                    break;
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

        private void SetFilterMaMatHang(int? maMatHang, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[MaMatHang] = FilterNullable(maMatHang, setFalse, p => p.MaMatHang == maMatHang);

            UpdateMainFilter();
        }

        private void SetFilterTenMatHang(string tenMatHang, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenMatHang] = FilterText(tenMatHang, setFalse, p => p.tMatHang.TenMatHang.Contains(tenMatHang));

            UpdateMainFilter();
        }

        private void SetFilterMaNhanVien(int? maNhanVien, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[MaNhanVien] = FilterNullable(maNhanVien, setFalse, p => p.MaNhanVien == maNhanVien);

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