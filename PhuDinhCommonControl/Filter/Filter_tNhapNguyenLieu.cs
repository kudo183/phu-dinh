using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class Filter_tNhapNguyenLieu
    {
        private Expression<Func<PhuDinhData.tNhapNguyenLieu, bool>> _filterNhaCungCap;
        private Expression<Func<PhuDinhData.tNhapNguyenLieu, bool>> _filterNguyenLieu;
        private Expression<Func<PhuDinhData.tNhapNguyenLieu, bool>> _filterNgay;
        private Expression<Func<PhuDinhData.tNhapNguyenLieu, bool>> _filterSoLuong;
        private bool _isClearAllData;

        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set
            {
                _isClearAllData = value;

                if (_isClearAllData == false)
                {
                    _filterNhaCungCap = (p => true);
                    _filterNguyenLieu = (p => true);
                    _filterNgay = (p => true);
                    _filterSoLuong = (p => true);   
                }
            }
        }

        public Filter_tNhapNguyenLieu()
        {
            IsClearAllData = false;
        }

        public Expression<Func<PhuDinhData.tNhapNguyenLieu, bool>> FilterNhaCungCap
        {
            get { return _filterNhaCungCap; }
            set
            {
                _filterNhaCungCap = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tNhapNguyenLieu, bool>> FilterNguyenLieu
        {
            get { return _filterNguyenLieu; }
            set
            {
                _filterNguyenLieu = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tNhapNguyenLieu, bool>> FilterNgay
        {
            get { return _filterNgay; }
            set
            {
                _filterNgay = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tNhapNguyenLieu, bool>> FilterGhiChu
        {
            get { return _filterSoLuong; }
            set
            {
                _filterSoLuong = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tNhapNguyenLieu, bool>> FilterChiPhi
        {
            get { return FilterNguyenLieu.And(FilterNhaCungCap).And(FilterNgay).And(FilterGhiChu); }
        }
    }
}
