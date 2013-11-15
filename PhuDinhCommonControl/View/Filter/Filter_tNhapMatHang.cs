using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class Filter_tNhapMatHang
    {
        private Expression<Func<PhuDinhData.tNhapMatHang, bool>> _filterNhanVien;
        private Expression<Func<PhuDinhData.tNhapMatHang, bool>> _filterMatHang;
        private Expression<Func<PhuDinhData.tNhapMatHang, bool>> _filterNgay;
        private Expression<Func<PhuDinhData.tNhapMatHang, bool>> _filterSoLuong;
        private bool _isClearAllData;

        public bool IsClearAllData
        {
            get { return _isClearAllData; }
            set
            {
                _isClearAllData = value;

                if (_isClearAllData == false)
                {
                    _filterNhanVien = (p => true);
                    _filterMatHang = (p => true);
                    _filterNgay = (p => true);
                    _filterSoLuong = (p => true);   
                }
            }
        }

        public Filter_tNhapMatHang()
        {
            IsClearAllData = false;
        }

        public Expression<Func<PhuDinhData.tNhapMatHang, bool>> FilterNhanVien
        {
            get { return _filterNhanVien; }
            set
            {
                _filterNhanVien = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tNhapMatHang, bool>> FilterMatHang
        {
            get { return _filterMatHang; }
            set
            {
                _filterMatHang = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tNhapMatHang, bool>> FilterNgay
        {
            get { return _filterNgay; }
            set
            {
                _filterNgay = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tNhapMatHang, bool>> FilterGhiChu
        {
            get { return _filterSoLuong; }
            set
            {
                _filterSoLuong = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tNhapMatHang, bool>> FilterChiPhi
        {
            get { return FilterMatHang.And(FilterNhanVien).And(FilterNgay).And(FilterGhiChu); }
        }
    }
}
