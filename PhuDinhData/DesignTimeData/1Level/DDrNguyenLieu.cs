﻿using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNguyenLieu
    {
        private static HeaderTextFilterModel _header_LoaiNguyenLieu;
        public static HeaderTextFilterModel Header_LoaiNguyenLieu
        {
            get
            {
                if (_header_LoaiNguyenLieu != null)
                {
                    return _header_LoaiNguyenLieu;
                }

                _header_LoaiNguyenLieu = new HeaderTextFilterModel(Constant.ColumnName_LoaiNguyenLieu);
                return _header_LoaiNguyenLieu;
            }
        }

        private static NguyenLieuViewModel _viewModel;
        public static NguyenLieuViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NguyenLieuViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rNguyenLieu Create(int i)
        {
            return new rNguyenLieu()
            {
                Ma = i,
                MaLoaiNguyenLieu = i,
                DuongKinh = i,
                rLoaiNguyenLieu = DDrLoaiNguyenLieu.Create(i),
                rLoaiNguyenLieuList = DDrLoaiNguyenLieu.ViewModel.Entity.ToList()
            };
        }
    }
}
