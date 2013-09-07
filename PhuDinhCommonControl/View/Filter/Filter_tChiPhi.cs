﻿using System;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    public class Filter_tChiPhi
    {
        private Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> _filterNhanVien;
        private Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> _filterLoaiChiPhi;
        private Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> _filterSoTien;
        private Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> _filterNgay;
        private Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> _filterGhiChu;
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
                    _filterLoaiChiPhi = (p => true);
                    _filterSoTien = (p => true);
                    _filterNgay = (p => true);
                    _filterGhiChu = (p => true);   
                }
            }
        }

        public Filter_tChiPhi()
        {
            IsClearAllData = false;
        }

        public Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> FilterNhanVien
        {
            get { return _filterNhanVien; }
            set
            {
                _filterNhanVien = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> FilterLoaiChiPhi
        {
            get { return _filterLoaiChiPhi; }
            set
            {
                _filterLoaiChiPhi = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> FilterSoTien
        {
            get { return _filterSoTien; }
            set
            {
                _filterSoTien = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> FilterNgay
        {
            get { return _filterNgay; }
            set
            {
                _filterNgay = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> FilterGhiChu
        {
            get { return _filterGhiChu; }
            set
            {
                _filterGhiChu = value;
                _isClearAllData = false;
            }
        }

        public Expression<Func<PhuDinhData.tChiPhiNhanVienGiaoHang, bool>> FilterChiPhi
        {
            get { return FilterLoaiChiPhi.And(FilterNhanVien).And(FilterNgay).And(FilterSoTien).And(FilterGhiChu); }
        }
    }
}
