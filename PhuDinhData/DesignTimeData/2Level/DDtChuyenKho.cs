using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChuyenKho
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

        private static HeaderTextFilterModel _header_KhoHangXuat;
        public static HeaderTextFilterModel Header_KhoHangXuat
        {
            get
            {
                if (_header_KhoHangXuat != null)
                {
                    return _header_KhoHangXuat;
                }

                _header_KhoHangXuat = new HeaderTextFilterModel(Constant.ColumnName_KhoHangXuat);
                return _header_KhoHangXuat;
            }
        }

        private static HeaderTextFilterModel _header_KhoHangNhap;
        public static HeaderTextFilterModel Header_KhoHangNhap
        {
            get
            {
                if (_header_KhoHangNhap != null)
                {
                    return _header_KhoHangNhap;
                }

                _header_KhoHangNhap = new HeaderTextFilterModel(Constant.ColumnName_KhoHangNhap);
                return _header_KhoHangNhap;
            }
        }

        private static ChuyenKhoViewModel _viewModel;
        public static ChuyenKhoViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChuyenKhoViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tChuyenKho Create(int i)
        {
            return new tChuyenKho()
            {
                Ma = i,
                MaNhanVien = i,
                MaKhoHangXuat = i,
                MaKhoHangNhap = i,
                Ngay = System.DateTime.Now,
                rNhanVien= DDrNhanVien.Create(i),
                rNhanVienList = DDrNhanVien.ViewModel.Entity.ToList(),
                rKhoHangXuat = DDrKhoHang.Create(i),
                rKhoHangNhap = DDrKhoHang.Create(i),
                rKhoHangList = DDrKhoHang.ViewModel.Entity.ToList()
            };
        }
    }
}
