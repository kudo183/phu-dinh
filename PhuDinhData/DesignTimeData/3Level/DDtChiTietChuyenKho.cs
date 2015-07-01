using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtChiTietChuyenKho
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

        private static ChiTietChuyenKhoViewModel _viewModel;
        public static ChiTietChuyenKhoViewModel ViewModel
        {
            get 
            {
                if(_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiTietChuyenKhoViewModel();

                const int count = 10;
                
                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tChiTietChuyenKho Create(int i)
        {
            return new tChiTietChuyenKho()
            {
                Ma = i,
                MaChuyenKho = i,
                MaMatHang = i,
                SoLuong = i,
                tChuyenKho = DDtChuyenKho.Create(i),
                tChuyenKhoList = DDtChuyenKho.ViewModel.Entity.ToList(),
                tMatHang = DDtMatHang.Create(i),
                tMatHangList = DDtMatHang.ViewModel.Entity.ToList()
            };
        }
    }
}
