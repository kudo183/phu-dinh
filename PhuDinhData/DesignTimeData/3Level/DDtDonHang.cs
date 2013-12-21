﻿using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtDonHang
    {
        private static DonHangViewModel _viewModel;
        public static DonHangViewModel ViewModel
        {
            get 
            {
                if(_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new DonHangViewModel();

                const int count = 10;
                
                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tDonHang Create(int i)
        {
            return new tDonHang()
            {
                Ma = i,
                MaChanh = i,
                MaKhachHang = i,
                Ngay = System.DateTime.Now,
                rKhachHang = DDrKhachHang.Create(i),
                rChanh = DDrChanh.Create(i),
                rKhachHangList = DDrKhachHang.rKhachHangs,
                rChanhList = DDrChanh.ViewModel.Entity.ToList()
            };
        }
    }
}
