﻿using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrMatHangNguyenLieu
    {
        private static MatHangNguyenLieuViewModel _viewModel;
        public static MatHangNguyenLieuViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new MatHangNguyenLieuViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rMatHangNguyenLieu Create(int i)
        {
            return new rMatHangNguyenLieu()
            {
                Ma = i,
                MaMatHang = i,
                MaNguyenLieu = i,
                tMatHang = DDtMatHang.Create(i),
                tMatHangList = DDtMatHang.ViewModel.Entity.ToList(),
                rNguyenLieu = DDrNguyenLieu.Create(i),
                rNguyenLieuList = DDrNguyenLieu.ViewModel.Entity.ToList(),
            };
        }
    }
}
