using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiTietDonHang
    {
        private static HeaderTextFilterModel _header_MatHang;
        public static HeaderTextFilterModel Header_MatHang
        {
            get
            {
                if (_header_MatHang != null)
                {
                    return _header_MatHang;
                }

                _header_MatHang = new HeaderTextFilterModel(Constant.ColumnName_MatHang);
                return _header_MatHang;
            }
        }

        private static ChiTietDonHangViewModel _viewModel;
        public static ChiTietDonHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiTietDonHangViewModel();

                const int count = 10;

                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tChiTietDonHang Create(int i)
        {
            return new tChiTietDonHang()
            {
                Ma = i,
                MaDonHang = i,
                MaMatHang = i,
                SoLuong = i * 10,
                tDonHang = DDtDonHang.Create(i),
                tMatHang = DDtMatHang.Create(i),
                tDonHangList = DDtDonHang.ViewModel.Entity.ToList(),
                tMatHangList = DDtMatHang.ViewModel.Entity.ToList()
            };
        }
    }
}
