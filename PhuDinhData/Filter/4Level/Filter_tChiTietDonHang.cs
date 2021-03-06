﻿using PhuDinhDataEntity;

namespace PhuDinhData.Filter
{
    public class Filter_tChiTietDonHang : FilterBase<tChiTietDonHang>
    {
        public const string MaMatHang = "MaMatHang";
        public const string TenMatHang = "TenMatHang";
        public const string MaDonHang = "MaDonHang";
        public const string TenDonHang = "TenDonHang";

        public Filter_tChiTietDonHang()
        {
            _filters[MaMatHang] = (p => true);
            _filters[TenMatHang] = (p => true);
            _filters[MaDonHang] = (p => true);
            _filters[TenDonHang] = (p => true);
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
                case MaDonHang:
                    SetFilterMaDonHang(value as int?, setFalse);
                    break;
                case TenDonHang:
                    SetFilterTenDonHang(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaMatHang(int? maMatHang, bool setFalse = false)
        {
            _filters[MaMatHang] = FilterNullable(maMatHang, setFalse, "MaMatHang");

            UpdateMainFilter();
        }

        private void SetFilterTenMatHang(string tenMatHang, bool setFalse = false)
        {
            _filters[TenMatHang] = FilterText(tenMatHang, setFalse, "tMatHang.TenMatHang");

            UpdateMainFilter();
        }

        private void SetFilterMaDonHang(int? maDonHang, bool setFalse = false)
        {
            _filters[MaDonHang] = FilterNullable(maDonHang, setFalse, "MaDonHang");

            UpdateMainFilter();
        }

        private void SetFilterTenDonHang(string tenDonHang, bool setFalse = false)
        {
            //_filters[TenDonHang] =
            //    FilterText(tenDonHang, setFalse, p => p.tDonHang.TenDonHang.Contains(tenDonHang));

            //UpdateMainFilter();
        }
    }
}
