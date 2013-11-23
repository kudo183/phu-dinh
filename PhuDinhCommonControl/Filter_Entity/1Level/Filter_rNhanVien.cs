using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class Filter_rNhanVien
    {
        private Expression<Func<PhuDinhData.rNhanVien, bool>> _filterPhuongTien;
        private Expression<Func<PhuDinhData.rNhanVien, bool>> _filterTenNhanVien;

        private bool _isClearAllData;

        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set
            {
                _isClearAllData = value;

                if (_isClearAllData == false)
                {
                    _filterPhuongTien = (p => true);
                    _filterTenNhanVien = (p => true);
                }
            }
        }

        public Filter_rNhanVien()
        {
            IsClearAllData = false;
        }

        public Expression<Func<PhuDinhData.rNhanVien, bool>> FilterPhuongTien
        {
            get { return _filterPhuongTien; }
            set
            {
                _filterPhuongTien = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.rNhanVien, bool>> FilterTenNhanVien
        {
            get { return _filterTenNhanVien; }
            set
            {
                _filterTenNhanVien = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.rNhanVien, bool>> FilterNhanVien
        {
            get { return FilterTenNhanVien.And(FilterPhuongTien); }
        }
    }
}
