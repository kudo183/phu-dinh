using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNhanVien
    {
        private static HeaderTextFilterModel _header_PhuongTien;
        public static HeaderTextFilterModel Header_PhuongTien
        {
            get
            {
                if (_header_PhuongTien != null)
                {
                    return _header_PhuongTien;
                }

                _header_PhuongTien = new HeaderTextFilterModel(Constant.ColumnName_PhuongTien);
                return _header_PhuongTien;
            }
        }

        private static NhanVienViewModel _viewModel;
        public static NhanVienViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NhanVienViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rNhanVien Create(int i)
        {
            return new rNhanVien()
            {
                Ma = i,
                MaPhuongTien = i,
                TenNhanVien = "Nhân viên " + i,
                rPhuongTien = DDrPhuongTien.Create(i),
                rPhuongTienList = DDrPhuongTien.ViewModel.Entity.ToList()
            };
        }
    }
}
