using System;

namespace PhuDinhData.Filter
{
    public class Filter_tDonHang : FilterBase<tDonHang>
    {
        public const string MaKhachHang = "MaKhachHang";
        public const string TenKhachHang = "TenKhachHang";
        public const string TenChanh = "TenChanh";
        public const string Ngay = "Ngay";
        public const string Xong = "Xong";

        public Filter_tDonHang()
        {
            IsClearAllData = false;

            _filters[MaKhachHang] = (p => true);
            _filters[TenKhachHang] = (p => true);
            _filters[TenChanh] = (p => true);
            _filters[Ngay] = (p => true);
            _filters[Xong] = (p => true);

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
                case TenChanh:
                    SetFilterTenChanh(value as string, setFalse);
                    break;
                case Ngay:
                    SetFilterNgay(value as DateTime?, setFalse);
                    break;
                case Xong:
                    SetFilterXong(value as bool?, setFalse);
                    break;
            }
        }

        private void SetFilterMaKhachHang(int? maKhachHang, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[MaKhachHang] = FilterNullable(maKhachHang, setFalse, p => p.MaKhachHang == maKhachHang);

            UpdateMainFilter();
        }

        private void SetFilterTenKhachHang(string tenKhachHang, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenKhachHang] = FilterText(tenKhachHang, setFalse, p => p.rKhachHang.TenKhachHang.Contains(tenKhachHang));

            UpdateMainFilter();
        }

        private void SetFilterTenChanh(string tenChanh, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenChanh] = FilterText(tenChanh, setFalse, p => p.rChanh.TenChanh.Contains(tenChanh));

            UpdateMainFilter();
        }

        private void SetFilterNgay(DateTime? date, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[Ngay] = FilterNullable(date, setFalse, p => p.Ngay == date);

            UpdateMainFilter();
        }

        private void SetFilterXong(bool? xong, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[Xong] = FilterNullable(xong, setFalse, p => p.Xong == xong);

            UpdateMainFilter();
        }
    }
}
