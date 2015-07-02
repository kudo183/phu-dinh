using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtNhapHang
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

        private static HeaderTextFilterModel _header_NhanVien;
        public static HeaderTextFilterModel Header_NhanVien
        {
            get
            {
                if (_header_NhanVien != null)
                {
                    return _header_NhanVien;
                }

                _header_NhanVien = new HeaderTextFilterModel(Constant.ColumnName_NhanVien);
                return _header_NhanVien;
            }
        }

        private static HeaderComboBoxFilterModel _header_NhaCungCap;
        public static HeaderComboBoxFilterModel Header_NhaCungCap
        {
            get
            {
                if (_header_NhaCungCap != null)
                {
                    return _header_NhaCungCap;
                }

                _header_NhaCungCap = new HeaderComboBoxFilterModel(Constant.ColumnName_NhanCungCap);
                return _header_NhaCungCap;
            }
        }

        private static HeaderComboBoxFilterModel _header_KhoHang;
        public static HeaderComboBoxFilterModel Header_KhoHang
        {
            get
            {
                if (_header_KhoHang != null)
                {
                    return _header_KhoHang;
                }

                _header_KhoHang = new HeaderComboBoxFilterModel(Constant.ColumnName_KhoHang);
                return _header_KhoHang;
            }
        }

        private static NhapHangViewModel _viewModel;
        public static NhapHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NhapHangViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tNhapHang Create(int i)
        {
            return new tNhapHang()
            {
                Ma = i,
                MaNhaCungCap = i,
                MaNhanVien = i,
                MaKhoHang = i,
                Ngay = System.DateTime.Now,
                rNhanVien= DDrNhanVien.Create(i),
                rNhanVienList = DDrNhanVien.ViewModel.Entity.ToList(),
                rNhaCungCap = DDrNhaCungCap.Create(i),
                rNhaCungCapList = DDrNhaCungCap.ViewModel.Entity.ToList(),
                rKhoHang = DDrKhoHang.Create(i),
                rKhoHangList = DDrKhoHang.ViewModel.Entity.ToList()
            };
        }
    }
}
