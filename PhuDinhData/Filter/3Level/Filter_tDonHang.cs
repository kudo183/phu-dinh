using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhData.Filter
{
    public class Filter_tDonHang
    {
        private Expression<Func<tDonHang, bool>> _filterMaKhachHang;
        private Expression<Func<tDonHang, bool>> _filterTenKhachHang;
        private Expression<Func<tDonHang, bool>> _filterTenChanh;
        private Expression<Func<tDonHang, bool>> _filterNgay;
        private Expression<Func<tDonHang, bool>> _filterXong;

        private bool _isClearAllData;
        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set { _isClearAllData = value; }
        }

        public Filter_tDonHang()
        {
            _isClearAllData = false;

            _filterMaKhachHang = (p => true);
            _filterTenKhachHang = (p => true);
            _filterTenChanh = (p => true);
            _filterNgay = (p => true);
            _filterXong = (p => true);
        }

        public void SetFilterMaKhachHang(int? maKhachHang, bool setFalse = false)
        {
            _isClearAllData = false;

            if (setFalse == true)
            {
                _filterMaKhachHang = (p => false);
            }
            else
            {
                if (maKhachHang == null)
                {
                    _filterMaKhachHang = (p => true);
                }
                else
                {
                    _filterMaKhachHang = (p => p.MaKhachHang == maKhachHang);
                }
            }

            UpdateMainFilter();
        }

        public void SetFilterXong(bool? xong, bool setFalse = false)
        {
            _isClearAllData = false;

            if (setFalse == true)
            {
                _filterXong = (p => false);
            }
            else
            {
                if (xong == null)
                {
                    _filterXong = (p => true);
                }
                else
                {
                    _filterNgay = (p => p.Xong == xong);
                }
            }

            UpdateMainFilter();
        }

        public void SetFilterTenChanh(string tenChanh, bool setFalse = false)
        {
            _isClearAllData = false;

            if (setFalse == true)
            {
                _filterTenChanh = (p => false);
            }
            else
            {
                if (string.IsNullOrEmpty(tenChanh))
                {
                    _filterTenChanh = (p => true);
                }
                else
                {
                    _filterTenChanh = (p => p.rChanh.TenChanh.Contains(tenChanh));
                }
            }

            UpdateMainFilter();
        }

        public void SetFilterTenKhachHang(string tenKhachHang, bool setFalse = false)
        {
            _isClearAllData = false;

            if (setFalse == true)
            {
                _filterTenKhachHang = (p => false);
            }
            else
            {
                if (string.IsNullOrEmpty(tenKhachHang))
                {
                    _filterTenKhachHang = (p => true);
                }
                else
                {
                    _filterTenKhachHang = (p => p.rKhachHang.TenKhachHang.Contains(tenKhachHang));
                }
            }

            UpdateMainFilter();
        }

        public void SetFilterNgay(DateTime? date, bool setFalse = false)
        {
            _isClearAllData = false;

            if (setFalse == true)
            {
                _filterNgay = (p => false);
            }
            else
            {
                if (date == null)
                {
                    _filterNgay = (p => true);
                }
                else
                {
                    _filterNgay = (p => p.Ngay == date);
                }
            }

            UpdateMainFilter();
        }

        private void UpdateMainFilter()
        {
            FilterDonHang = _filterMaKhachHang.And(_filterXong).And(_filterTenKhachHang).And(_filterNgay).And(_filterTenChanh);
        }

        public Expression<Func<tDonHang, bool>> FilterDonHang { get; private set; }
    }
}
