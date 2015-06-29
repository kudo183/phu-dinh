using PhuDinhDataEntity;
using System;

namespace PhuDinhData.Filter
{
    public class Filter_tChuyenKho : FilterBase<tChuyenKho>
    {
        public const string MaKhoHangNhap = "MaKhoHangNhap";
        public const string TenKhoHangNhap = "TenKhoHangNhap";
        public const string MaKhoHangXuat = "MaKhoHangXuat";
        public const string TenKhoHangXuat = "TenKhoHangXuat";
        public const string MaNhanVien = "MaNhanVien";
        public const string TenNhanVien = "TenNhanVien";
        public const string Ngay = "Ngay";

        public Filter_tChuyenKho()
        {
            _filters[MaKhoHangNhap] = (p => true);
            _filters[TenKhoHangNhap] = (p => true);
            _filters[MaKhoHangXuat] = (p => true);
            _filters[TenKhoHangXuat] = (p => true);
            _filters[MaNhanVien] = (p => true);
            _filters[TenNhanVien] = (p => true);
            _filters[Ngay] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaKhoHangNhap:
                    SetFilterMaKhoHangNhap(value as int?, setFalse);
                    break;
                case TenKhoHangNhap:
                    SetFilterTenKhoHangNhap(value as string, setFalse);
                    break;
                case MaKhoHangXuat:
                    SetFilterMaKhoHangXuat(value as int?, setFalse);
                    break;
                case TenKhoHangXuat:
                    SetFilterTenKhoHangXuat(value as string, setFalse);
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

        private void SetFilterMaKhoHangNhap(int? maKhoHangNhap, bool setFalse = false)
        {
            _filters[MaKhoHangNhap] = FilterNullable(MaKhoHangNhap, setFalse, "MaKhoHangNhap");

            UpdateMainFilter();
        }

        private void SetFilterTenKhoHangNhap(string tenKhoHangNhap, bool setFalse = false)
        {
            _filters[TenKhoHangNhap] = FilterText(tenKhoHangNhap, setFalse, "rKhoHangNhap.TenKho");

            UpdateMainFilter();
        }

        private void SetFilterMaKhoHangXuat(int? maKhoHangXuat, bool setFalse = false)
        {
            _filters[MaKhoHangXuat] = FilterNullable(MaKhoHangXuat, setFalse, "MaKhoHangXuat");

            UpdateMainFilter();
        }

        private void SetFilterTenKhoHangXuat(string tenKhoHangXuat, bool setFalse = false)
        {
            _filters[TenKhoHangXuat] = FilterText(tenKhoHangXuat, setFalse, "rKhoHangXuat.TenKho");

            UpdateMainFilter();
        }

        private void SetFilterMaNhanVien(int? maNhanVien, bool setFalse = false)
        {
            _filters[MaNhanVien] = FilterNullable(maNhanVien, setFalse, "MaNhanVien");

            UpdateMainFilter();
        }

        private void SetFilterTenNhanVien(string tenNhanVien, bool setFalse = false)
        {
            _filters[TenNhanVien] = FilterText(tenNhanVien, setFalse, "rNhanVien.TenNhanVien");

            UpdateMainFilter();
        }

        private void SetFilterNgay(DateTime? date, bool setFalse = false)
        {
            _filters[Ngay] = FilterNullable(date, setFalse, "Ngay");

            UpdateMainFilter();
        }
    }
}
