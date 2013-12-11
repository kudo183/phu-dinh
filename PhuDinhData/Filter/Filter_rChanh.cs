using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhData.Filter
{
    public class Filter_rChanh
    {
        private Expression<Func<rChanh, bool>> _filterBaiXe;
        private Expression<Func<rChanh, bool>> _filterTenChanh;

        private bool _isClearAllData;

        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set
            {
                _isClearAllData = value;

                if (_isClearAllData == false)
                {
                    _filterBaiXe = (p => true);
                    _filterTenChanh = (p => true);
                }
            }
        }

        public Filter_rChanh()
        {
            IsClearAllData = false;
        }

        public Expression<Func<rChanh, bool>> FilterBaiXe
        {
            get { return _filterBaiXe; }
            set
            {
                _filterBaiXe = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<rChanh, bool>> FilterTenChanh
        {
            get { return _filterTenChanh; }
            set
            {
                _filterTenChanh = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<rChanh, bool>> FilterChanh
        {
            get { return FilterTenChanh.And(FilterBaiXe); }
        }
    }
}
