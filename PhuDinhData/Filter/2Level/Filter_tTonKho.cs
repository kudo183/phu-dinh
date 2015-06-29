using PhuDinhDataEntity;
using System;

namespace PhuDinhData.Filter
{
    public class Filter_tTonKho : FilterBase<tTonKho>
    {
        public const string MaMatHang = "MaMatHang";
        public const string TenMatHang = "TenMatHang";
        public const string MaKhoHang = "MaKhoHang";
        public const string TenKhoHang = "TenKhoHang";
        public const string Ngay = "Ngay";

        public Filter_tTonKho()
        {
            _filters[MaMatHang] = (p => true);
            _filters[TenMatHang] = (p => true);
            _filters[MaKhoHang] = (p => true);
            _filters[TenKhoHang] = (p => true);
            _filters[Ngay] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            _filtersValue[key] = value;

            switch (key)
            {
                case MaMatHang:
                    SetFilterMaMatHang(value as int?, setFalse);
                    break;
                case TenMatHang:
                    SetFilterTenMatHang(value as string, setFalse);
                    break;
                case MaKhoHang:
                    SetFilterMaKhoHang(value as int?, setFalse);
                    break;
                case TenKhoHang:
                    SetFilterTenKhoHang(value as string, setFalse);
                    break;
                case Ngay:
                    SetFilterNgay(value as DateTime?, setFalse);
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

        private void SetFilterMaKhoHang(int? maKhoHang, bool setFalse = false)
        {
            _filters[MaKhoHang] = FilterNullable(maKhoHang, setFalse, "MaKhoHang");

            UpdateMainFilter();
        }

        private void SetFilterTenKhoHang(string tenKhoHang, bool setFalse = false)
        {
            _filters[TenKhoHang] = FilterText(tenKhoHang, setFalse, "rKhoHang.TenKho");

            UpdateMainFilter();
        }

        private void SetFilterNgay(DateTime? date, bool setFalse = false)
        {
            _filters[Ngay] = FilterNullable(date, setFalse, "Ngay");

            UpdateMainFilter();
        }
    }
}
