using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class Filter_rNguyenLieu
    {
        private Expression<Func<PhuDinhData.rNguyenLieu, bool>> _filterLoaiNguyenLieu;
        private Expression<Func<PhuDinhData.rNguyenLieu, bool>> _filterDuongKinh;

        private bool _isClearAllData;

        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set
            {
                _isClearAllData = value;

                if (_isClearAllData == false)
                {
                    _filterLoaiNguyenLieu = (p => true);
                    _filterDuongKinh = (p => true);
                }
            }
        }

        public Filter_rNguyenLieu()
        {
            IsClearAllData = false;
        }

        public Expression<Func<PhuDinhData.rNguyenLieu, bool>> FilterLoaiNguyenLieu
        {
            get { return _filterLoaiNguyenLieu; }
            set
            {
                _filterLoaiNguyenLieu = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.rNguyenLieu, bool>> FilterDuongKinh
        {
            get { return _filterDuongKinh; }
            set
            {
                _filterDuongKinh = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.rNguyenLieu, bool>> FilterNguyenLieu
        {
            get { return FilterLoaiNguyenLieu.And(FilterDuongKinh); }
        }
    }
}
