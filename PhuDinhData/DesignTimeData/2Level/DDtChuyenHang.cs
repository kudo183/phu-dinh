using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChuyenHang
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

        private static ChuyenHangViewModel _viewModel;
        public static ChuyenHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChuyenHangViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tChuyenHang Create(int i)
        {
            return new tChuyenHang()
            {
                Ma = i,
                MaNhanVienGiaoHang = i,
                Ngay = System.DateTime.Now,
                Gio = System.DateTime.Now.TimeOfDay,
                rNhanVien = DDrNhanVien.Create(i),
                rNhanVienList = DDrNhanVien.ViewModel.Entity.ToList()
            };
        }
    }
}
