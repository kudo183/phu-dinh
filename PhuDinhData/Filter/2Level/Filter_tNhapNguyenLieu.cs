using PhuDinhDataEntity;
using System;

namespace PhuDinhData.Filter
{
    public class Filter_tNhapNguyenLieu : FilterBase<tNhapNguyenLieu>
    {
        public const string MaNguyenLieu = "MaNguyenLieu";
        public const string TenNguyenLieu = "TenNguyenLieu";
        public const string MaNhaCungCap = "MaNhaCungCap";
        public const string TenNhaCungCap = "TenNhaCungCap";
        public const string Ngay = "Ngay";

        public Filter_tNhapNguyenLieu()
        {
            _filters[MaNguyenLieu] = (p => true);
            _filters[TenNguyenLieu] = (p => true);
            _filters[MaNhaCungCap] = (p => true);
            _filters[TenNhaCungCap] = (p => true);
            _filters[Ngay] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaNguyenLieu:
                    SetFilterMaNguyenLieu(value as int?, setFalse);
                    break;
                case TenNguyenLieu:
                    SetFilterTenNguyenLieu(value as string, setFalse);
                    break;
                case MaNhaCungCap:
                    SetFilterMaNhaCungCap(value as int?, setFalse);
                    break;
                case TenNhaCungCap:
                    SetFilterTenNhaCungCap(value as string, setFalse);
                    break;
                case Ngay:
                    SetFilterNgay(value as DateTime?, setFalse);
                    break;
            }
        }

        private void SetFilterMaNguyenLieu(int? maNguyenLieu, bool setFalse = false)
        {
            _filters[MaNguyenLieu] = FilterNullable(maNguyenLieu, setFalse, p => p.MaNguyenLieu == maNguyenLieu);

            UpdateMainFilter();
        }

        private void SetFilterTenNguyenLieu(string tenNguyenLieu, bool setFalse = false)
        {
            _filters[TenNguyenLieu] = FilterText(tenNguyenLieu, setFalse, "rNguyenLieu.TenNguyenLieu");

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

        private void SetFilterNgay(DateTime? date, bool setFalse = false)
        {
            _filters[Ngay] = FilterNullable(date, setFalse, p => p.Ngay == date);

            UpdateMainFilter();
        }
    }
}
