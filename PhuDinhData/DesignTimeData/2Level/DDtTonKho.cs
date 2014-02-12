﻿using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtTonKho
    {
        private static TonKhoViewModel _viewModel;
        public static TonKhoViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new TonKhoViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tTonKho Create(int i)
        {
            return new tTonKho()
            {
                Ma = i,
                MaMatHang = i,
                MaKhoHang = i,
                Ngay = System.DateTime.Now,
                SoLuong = i,
                tMatHang= DDtMatHang.Create(i),
                tMatHangList = DDtMatHang.ViewModel.Entity.ToList(),
                rKhoHang = DDrKhoHang.Create(i),
                rKhoHangList = DDrKhoHang.ViewModel.Entity.ToList()
            };
        }
    }
}