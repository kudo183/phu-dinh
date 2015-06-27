using PhuDinhDataEntity;
using System;

namespace PhuDinhData.Filter
{
    public class Filter_tNhapHang : FilterBase<tNhapHang>
    {
        public const string MaKhoHang = "MaKhoHang";
        public const string TenKhoHang = "TenKhoHang";
        public const string MaNhaCungCap = "MaNhaCungCap";
        public const string TenNhaCungCap = "TenNhaCungCap";
        public const string MaNhanVien = "MaNhanVien";
        public const string TenNhanVien = "TenNhanVien";
        public const string Ngay = "Ngay";

        public Filter_tNhapHang()
        {
            _filters[MaKhoHang] = (p => true);
            _filters[TenKhoHang] = (p => true);
            _filters[MaNhaCungCap] = (p => true);
            _filters[TenNhaCungCap] = (p => true);
            _filters[MaNhanVien] = (p => true);
            _filters[TenNhanVien] = (p => true);
            _filters[Ngay] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaKhoHang:
                    SetFilterMaKhoHang(value as int?, setFalse);
                    break;
                case TenKhoHang:
                    SetFilterTenKhoHang(value as string, setFalse);
                    break;
                case MaNhaCungCap:
                    SetFilterMaNhaCungCap(value as int?, setFalse);
                    break;
                case TenNhaCungCap:
                    SetFilterTenNhaCungCap(value as string, setFalse);
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

        private void SetFilterMaKhoHang(int? maKhoHang, bool setFalse = false)
        {
            _filters[MaKhoHang] = FilterNullable(maKhoHang, setFalse, p => p.MaKhoHang == maKhoHang);

            UpdateMainFilter();
        }

        private void SetFilterTenKhoHang(string tenKhoHang, bool setFalse = false)
        {
            _filters[TenKhoHang] = FilterText(tenKhoHang, setFalse, "rKhoHang.TenKho");

            UpdateMainFilter();
        }

        private void SetFilterMaNhaCungCap(int? maNhaCungCap, bool setFalse = false)
        {
            _filters[MaNhaCungCap] = FilterNullable(maNhaCungCap, setFalse, p => p.MaNhaCungCap == maNhaCungCap);

            UpdateMainFilter();
        }

        private void SetFilterTenNhaCungCap(string tenNhaCungCap, bool setFalse = false)
        {
            _filters[TenNhaCungCap] = FilterText(tenNhaCungCap, setFalse, "rNhaCungCap.TenNhaCungCap");

            UpdateMainFilter();
        }

        private void SetFilterMaNhanVien(int? maNhanVien, bool setFalse = false)
        {
            _filters[MaNhanVien] = FilterNullable(maNhanVien, setFalse, p => p.MaNhanVien == maNhanVien);

            UpdateMainFilter();
        }

        private void SetFilterTenNhanVien(string tenNhanVien, bool setFalse = false)
        {
            _filters[TenNhanVien] = FilterText(tenNhanVien, setFalse, "rNhanVien.TenNhanVien");

            UpdateMainFilter();
        }

        private void SetFilterNgay(DateTime? date, bool setFalse = false)
        {
            _filters[Ngay] = FilterNullable(date, setFalse, p => p.Ngay == date);

            UpdateMainFilter();
        }
    }
}
