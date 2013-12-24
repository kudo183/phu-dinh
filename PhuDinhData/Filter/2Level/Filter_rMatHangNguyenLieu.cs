using System;

namespace PhuDinhData.Filter
{
    public class Filter_rMatHangNguyenLieu : FilterBase<rMatHangNguyenLieu>
    {
        public const string MaMatHang = "MaMatHang";
        public const string TenMatHang = "TenMatHang";
        public const string MaNguyenLieu = "MaNguyenLieu";
        public const string TenNguyenLieu = "TenNguyenLieu";

        public Filter_rMatHangNguyenLieu()
        {
            IsClearAllData = false;

            _filters[MaMatHang] = (p => true);
            _filters[TenMatHang] = (p => true);
            _filters[MaNguyenLieu] = (p => true);
            _filters[TenNguyenLieu] = (p => true);

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
                case MaNguyenLieu:
                    SetFilterMaNguyenLieu(value as int?, setFalse);
                    break;
                case TenNguyenLieu:
                    SetFilterTenNguyenLieu(value as string, setFalse);
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

        private void SetFilterMaNguyenLieu(int? maNguyenLieu, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[MaNguyenLieu] = FilterNullable(maNguyenLieu, setFalse, p => p.MaNguyenLieu == maNguyenLieu);

            UpdateMainFilter();
        }

        private void SetFilterTenNguyenLieu(string tenNguyenLieu, bool setFalse = false)
        {
            IsClearAllData = false;

            _filters[TenNguyenLieu] = FilterText(tenNguyenLieu, setFalse, p => p.rNguyenLieu.TenNguyenLieu.Contains(tenNguyenLieu));

            UpdateMainFilter();
        }
    }
}
