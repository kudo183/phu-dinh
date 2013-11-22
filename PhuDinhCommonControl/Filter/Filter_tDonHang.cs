using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class Filter_tDonHang
    {
        private Expression<Func<PhuDinhData.tDonHang, bool>> _filterKhachHang;
        private Expression<Func<PhuDinhData.tDonHang, bool>> _filterChanh;
        private Expression<Func<PhuDinhData.tDonHang, bool>> _filterNgay;
        private Expression<Func<PhuDinhData.tDonHang, bool>> _filterXong;
        private bool _isClearAllData;

        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set { _isClearAllData = value; }
        }

        public Filter_tDonHang()
        {
            FilterKhachHang = (p => true);
            FilterChanh = (p => true);
            FilterNgay = (p => true);
            FilterXong = (p => true);
        }

        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterKhachHang
        {
            get { return _filterKhachHang; }
            set
            {
                _filterKhachHang = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterChanh
        {
            get { return _filterChanh; }
            set
            {
                _filterChanh = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterNgay
        {
            get { return _filterNgay; }
            set
            {
                _filterNgay = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterXong
        {
            get { return _filterXong; }
            set
            {
                _filterXong = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang
        {
            get { return FilterXong.And(FilterKhachHang).And(FilterNgay).And(FilterChanh); }
        }
    }
}
