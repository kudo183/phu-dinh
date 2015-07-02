using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtTonKho
    {
        private static HeaderDateFilterModel _header_Ngay;
        public static HeaderDateFilterModel Header_Ngay
        {
            get
            {
                if (_header_Ngay != null)
                {
                    return _header_Ngay;
                }

                _header_Ngay = new HeaderDateFilterModel(Constant.ColumnName_Ngay);
                return _header_Ngay;
            }
        }

        private static HeaderTextFilterModel _header_KhoHang;
        public static HeaderTextFilterModel Header_KhoHang
        {
            get
            {
                if (_header_KhoHang != null)
                {
                    return _header_KhoHang;
                }

                _header_KhoHang = new HeaderTextFilterModel(Constant.ColumnName_KhoHang);
                return _header_KhoHang;
            }
        }

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
