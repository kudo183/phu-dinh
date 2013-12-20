using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhData.Filter
{
    public class Filter_rChanh
    {
        private Expression<Func<rChanh, bool>> _filterMaBaiXe;
        private Expression<Func<rChanh, bool>> _filterDiaDiemBaiXe;
        private Expression<Func<rChanh, bool>> _filterTenChanh;

        public bool IsClearAllData { get; set; }

        public Filter_rChanh()
        {
            IsClearAllData = false;

            _filterMaBaiXe = (p => true);
            _filterDiaDiemBaiXe = (p => true);
            _filterTenChanh = (p => true);

            UpdateMainFilter();
        }

        public void SetFilterMaBaiXe(int? maBaiXe, bool setFalse = false)
        {
            IsClearAllData = false;

            if (setFalse == true)
            {
                _filterMaBaiXe = (p => false);
            }
            else
            {
                if (maBaiXe == null)
                {
                    _filterMaBaiXe = (p => true);
                }
                else
                {
                    _filterMaBaiXe = (p => p.MaBaiXe == maBaiXe);
                }
            }

            UpdateMainFilter();
        }

        public void SetFilterDiaDiemBaiXe(string diaDiemBaiXe, bool setFalse = false)
        {
            IsClearAllData = false;

            if (setFalse == true)
            {
                _filterDiaDiemBaiXe = (p => false);
            }
            else
            {
                if (string.IsNullOrEmpty(diaDiemBaiXe))
                {
                    _filterDiaDiemBaiXe = (p => true);
                }
                else
                {
                    _filterDiaDiemBaiXe = (p => p.rBaiXe.DiaDiemBaiXe.Contains(diaDiemBaiXe));
                }
            }

            UpdateMainFilter();
        }

        public void SetFilterTenChanh(string tenChanh, bool setFalse = false)
        {
            IsClearAllData = false;

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
                    _filterTenChanh = (p => p.TenChanh.Contains(tenChanh));
                }
            }

            UpdateMainFilter();
        }

        private void UpdateMainFilter()
        {
            FilterChanh = _filterTenChanh.And(_filterMaBaiXe).And(_filterDiaDiemBaiXe);
        }

        public Expression<Func<rChanh, bool>> FilterChanh { get; private set; }
    }
}
