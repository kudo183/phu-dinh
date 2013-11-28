using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class Filter_rKhachHangChanh
    {
        private Expression<Func<PhuDinhData.rKhachHangChanh, bool>> _filterKhachHang;
        private Expression<Func<PhuDinhData.rKhachHangChanh, bool>> _filterChanh;
        private bool _isClearAllData;

        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set { _isClearAllData = value; }
        }

        public Filter_rKhachHangChanh()
        {
            FilterKhachHang = (p => true);
            FilterChanh = (p => true);
        }

        public Expression<Func<PhuDinhData.rKhachHangChanh, bool>> FilterKhachHang
        {
            get { return _filterKhachHang; }
            set
            {
                _filterKhachHang = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.rKhachHangChanh, bool>> FilterChanh
        {
            get { return _filterChanh; }
            set
            {
                _filterChanh = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.rKhachHangChanh, bool>> FilterKhachHangChanh
        {
            get { return FilterKhachHang.And(FilterChanh); }
        }
    }
}
