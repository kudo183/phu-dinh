using PhuDinhDataEntity;
using System;

namespace PhuDinhData.Filter
{
    public class Filter_tToaHang : FilterBase<tToaHang>
    {
        public const string MaKhachHang = "MaKhachHang";
        public const string TenKhachHang = "TenKhachHang";
        public const string Ngay = "Ngay";

        public Filter_tToaHang()
        {
            _filters[MaKhachHang] = (p => true);
            _filters[TenKhachHang] = (p => true);
            _filters[Ngay] = (p => true);

            UpdateMainFilter();
        }

        public override void SetFilter(string key, object value, bool setFalse = false)
        {
            switch (key)
            {
                case MaKhachHang:
                    SetFilterMaKhachHang(value as int?, setFalse);
                    break;
                case TenKhachHang:
                    SetFilterTenKhachHang(value as string, setFalse);
                    break;
                case Ngay:
                    SetFilterNgay(value as DateTime?, setFalse);
                    break;
            }
        }

        private void SetFilterMaKhachHang(int? maKhachHang, bool setFalse = false)
        {
            _filters[MaKhachHang] = FilterNullable(maKhachHang, setFalse, "MaKhachHang");

            UpdateMainFilter();
        }

        private void SetFilterTenKhachHang(string tenKhachHang, bool setFalse = false)
        {
            _filters[TenKhachHang] = FilterText(tenKhachHang, setFalse, "rKhachHang.TenKhachHang");

            UpdateMainFilter();
        }

        private void SetFilterNgay(DateTime? date, bool setFalse = false)
        {
            _filters[Ngay] = FilterNullable(date, setFalse, "Ngay");

            UpdateMainFilter();
        }
    }
}
