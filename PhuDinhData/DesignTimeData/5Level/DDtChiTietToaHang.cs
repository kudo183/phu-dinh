using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiTietToaHang
    {
        private static HeaderTextFilterModel _header_ChiTietDonHang;
        public static HeaderTextFilterModel Header_ChiTietDonHang
        {
            get
            {
                if (_header_ChiTietDonHang != null)
                {
                    return _header_ChiTietDonHang;
                }

                _header_ChiTietDonHang = new HeaderTextFilterModel(Constant.ColumnName_ChiTietDonHang);
                return _header_ChiTietDonHang;
            }
        }

        private static ChiTietToaHangViewModel _viewModel;
        public static ChiTietToaHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiTietToaHangViewModel();

                const int count = 10;

                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tChiTietToaHang Create(int i)
        {
            return new tChiTietToaHang()
            {
                Ma = i,
                MaChiTietDonHang = i,
                MaToaHang = i,
                GiaTien = i,
                tChiTietDonHang = DDtChiTietDonHang.Create(i),
                tToaHang = DDtToaHang.Create(i),
                tChiTietDonHangList = DDtChiTietDonHang.ViewModel.Entity.ToList(),
                tToaHangList = DDtToaHang.ViewModel.Entity.ToList()
            };
        }
    }
}
