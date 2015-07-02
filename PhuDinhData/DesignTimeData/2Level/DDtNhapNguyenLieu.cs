using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtNhapNguyenLieu
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

        private static HeaderTextFilterModel _header_NhanCungCap;
        public static HeaderTextFilterModel Header_NhanCungCap
        {
            get
            {
                if (_header_NhanCungCap != null)
                {
                    return _header_NhanCungCap;
                }

                _header_NhanCungCap = new HeaderTextFilterModel(Constant.ColumnName_NhanCungCap);
                return _header_NhanCungCap;
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

        private static NhapNguyenLieuViewModel _viewModel;
        public static NhapNguyenLieuViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NhapNguyenLieuViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tNhapNguyenLieu Create(int i)
        {
            return new tNhapNguyenLieu()
            {
                Ma = i,
                MaNhaCungCap = i,
                MaNguyenLieu = i,
                Ngay = System.DateTime.Now,
                rNguyenLieu= DDrNguyenLieu.Create(i),
                rNguyenLieuList = DDrNguyenLieu.ViewModel.Entity.ToList(),
                rNhaCungCap = DDrNhaCungCap.Create(i),
                rNhaCungCapList = DDrNhaCungCap.ViewModel.Entity.ToList()
            };
        }
    }
}
