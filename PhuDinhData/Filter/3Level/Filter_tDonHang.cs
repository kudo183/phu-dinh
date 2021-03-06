﻿using PhuDinhDataEntity;
using System;

namespace PhuDinhData.Filter
{
    public class Filter_tDonHang : FilterBase<tDonHang>
    {
        public const string MaKhachHang = "MaKhachHang";
        public const string TenKhachHang = "TenKhachHang";
        public const string MaChanh = "MaChanh";
        public const string TenChanh = "TenChanh";
        public const string Ngay = "Ngay";
        public const string Xong = "Xong";
        public const string MaKhoHang = "MaKhoHang";
        public const string TenKhoHang = "TenKhoHang";

        public Filter_tDonHang()
        {
            _filters[MaKhachHang] = (p => true);
            _filters[TenKhachHang] = (p => true);
            _filters[MaChanh] = (p => true);
            _filters[TenChanh] = (p => true);
            _filters[Ngay] = (p => true);
            _filters[Xong] = (p => true);
            _filters[TenKhoHang] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaKhachHang:
                    SetFilterMaKhachHang(value as int?, setFalse);
                    break;
                case MaKhoHang:
                    SetFilterMaKhoHang(value as int?, setFalse);
                    break;
                case TenKhachHang:
                    SetFilterTenKhachHang(value as string, setFalse);
                    break;
                case MaChanh:
                    SetFilterMaChanh(value as int?, setFalse);
                    break;
                case TenChanh:
                    SetFilterTenChanh(value as string, setFalse);
                    break;
                case Ngay:
                    SetFilterNgay(value as DateTime?, setFalse);
                    break;
                case Xong:
                    SetFilterXong(value as bool?, setFalse);
                    break;
                case TenKhoHang:
                    SetFilterTenKhoHang(value as string, setFalse);
                    break;
            }
        }

        private void SetFilterMaKhachHang(int? maKhachHang, bool setFalse = false)
        {
            _filters[MaKhachHang] = FilterNullable(maKhachHang, setFalse, "MaKhachHang");

            UpdateMainFilter();
        }

        private void SetFilterMaKhoHang(int? maKhoHang, bool setFalse = false)
        {
            _filters[MaKhoHang] = FilterNullable(maKhoHang, setFalse, "MaKhoHang");

            UpdateMainFilter();
        }

        private void SetFilterTenKhachHang(string tenKhachHang, bool setFalse = false)
        {
            _filters[TenKhachHang] = FilterText(tenKhachHang, setFalse, "rKhachHang.TenKhachHang");

            UpdateMainFilter();
        }

        private void SetFilterMaChanh(int? maChanh, bool setFalse = false)
        {
            _filters[MaChanh] = FilterNullable(maChanh, setFalse, "MaChanh");

            UpdateMainFilter();
        }

        private void SetFilterTenChanh(string tenChanh, bool setFalse = false)
        {
            _filters[TenChanh] = FilterText(tenChanh, setFalse, "rChanh.TenChanh");

            UpdateMainFilter();
        }

        private void SetFilterNgay(DateTime? date, bool setFalse = false)
        {
            _filters[Ngay] = FilterNullable(date, setFalse, "Ngay");

            UpdateMainFilter();
        }

        private void SetFilterTenKhoHang(string tenKhoHang, bool setFalse = false)
        {
            _filters[TenKhoHang] = FilterText(tenKhoHang, setFalse, "rKhoHang.TenKho");

            UpdateMainFilter();
        }

        private void SetFilterXong(bool? xong, bool setFalse = false)
        {
            _filters[Xong] = FilterNullable(xong, setFalse, "Xong");

            UpdateMainFilter();
        }
    }
}
