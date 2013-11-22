using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class Filter_rDiaDiem
    {
        private Expression<Func<PhuDinhData.rDiaDiem, bool>> _filterNuoc;
        private Expression<Func<PhuDinhData.rDiaDiem, bool>> _filterTinh;

        private bool _isClearAllData;

        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set
            {
                _isClearAllData = value;

                if (_isClearAllData == false)
                {
                    _filterNuoc = (p => true);
                    _filterTinh = (p => true);
                }
            }
        }

        public Filter_rDiaDiem()
        {
            IsClearAllData = false;
        }

        public Expression<Func<PhuDinhData.rDiaDiem, bool>> FilterNuoc
        {
            get { return _filterNuoc; }
            set
            {
                _filterNuoc = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.rDiaDiem, bool>> FilterTinh
        {
            get { return _filterTinh; }
            set
            {
                _filterTinh = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.rDiaDiem, bool>> FilterDiaDiem
        {
            get { return FilterTinh.And(FilterNuoc); }
        }
    }
}
