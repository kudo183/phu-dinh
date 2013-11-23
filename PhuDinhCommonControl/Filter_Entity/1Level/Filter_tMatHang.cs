using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class Filter_tMatHang
    {
        private Expression<Func<PhuDinhData.tMatHang, bool>> _filterLoai;
        private Expression<Func<PhuDinhData.tMatHang, bool>> _filterTenMatHang;
        private Expression<Func<PhuDinhData.tMatHang, bool>> _filterSoKy;
        private Expression<Func<PhuDinhData.tMatHang, bool>> _filterSoMet;
        private Expression<Func<PhuDinhData.tMatHang, bool>> _filterTenMatHangDayDu;

        private bool _isClearAllData;

        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set
            {
                _isClearAllData = value;

                if (_isClearAllData == false)
                {
                    _filterLoai = (p => true);
                    _filterTenMatHang = (p => true);
                    _filterSoKy = (p => true);
                    _filterSoMet = (p => true);
                    _filterTenMatHangDayDu = (p => true);
                }
            }
        }

        public Filter_tMatHang()
        {
            IsClearAllData = false;
        }

        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterLoai
        {
            get { return _filterLoai; }
            set
            {
                _filterLoai = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterTenMatHang
        {
            get { return _filterTenMatHang; }
            set
            {
                _filterTenMatHang = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterSoKy
        {
            get { return _filterSoKy; }
            set
            {
                _filterSoKy = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterSoMet
        {
            get { return _filterSoMet; }
            set
            {
                _filterSoMet = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterTenMatHangDayDu
        {
            get { return _filterTenMatHangDayDu; }
            set
            {
                _filterTenMatHangDayDu = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tMatHang, bool>> FiltetMatHang
        {
            get { return FilterTenMatHang.And(FilterTenMatHangDayDu).And(FilterLoai).And(FilterSoKy).And(FilterSoMet); }
        }
    }
}
