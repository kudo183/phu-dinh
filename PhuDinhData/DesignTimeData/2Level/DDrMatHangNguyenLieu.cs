using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrMatHangNguyenLieu
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

        private static HeaderTextFilterModel _header_NguyenLieu;
        public static HeaderTextFilterModel Header_NguyenLieu
        {
            get
            {
                if (_header_NguyenLieu != null)
                {
                    return _header_NguyenLieu;
                }

                _header_NguyenLieu = new HeaderTextFilterModel(Constant.ColumnName_NguyenLieu);
                return _header_NguyenLieu;
            }
        }

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
