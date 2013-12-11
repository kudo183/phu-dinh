using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhData.Filter
{
    public class Filter_tDonHang
    {
        private Expression<Func<tDonHang, bool>> _filterMaKhachHang;
        private Expression<Func<tDonHang, bool>> _filterTenKhachHang;
        private Expression<Func<tDonHang, bool>> _filterChanh;
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
            FilterMaKhachHang = (p => true);
            FilterTenKhachHang = (p => true);
            FilterChanh = (p => true);
            FilterNgay = (p => true);
            FilterXong = (p => true);
        }

        public Expression<Func<tDonHang, bool>> FilterMaKhachHang
        {
            get { return _filterMaKhachHang; }
            set
            {
                _filterMaKhachHang = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<tDonHang, bool>> FilterTenKhachHang
        {
            get { return _filterTenKhachHang; }
            set
            {
                _filterTenKhachHang = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<tDonHang, bool>> FilterChanh
        {
            get { return _filterChanh; }
            set
            {
                _filterChanh = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<tDonHang, bool>> FilterNgay
        {
            get { return _filterNgay; }
            set
            {
                _filterNgay = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<tDonHang, bool>> FilterXong
        {
            get { return _filterXong; }
            set
            {
                _filterXong = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<tDonHang, bool>> FilterDonHang
        {
            get { return FilterMaKhachHang.And(FilterXong).And(FilterTenKhachHang).And(FilterNgay).And(FilterChanh); }
        }
    }
}
