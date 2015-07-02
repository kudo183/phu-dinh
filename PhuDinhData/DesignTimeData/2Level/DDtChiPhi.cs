using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiPhi
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

        private static HeaderTextFilterModel _header_LoaiChiPhi;
        public static HeaderTextFilterModel Header_LoaiChiPhi
        {
            get
            {
                if (_header_LoaiChiPhi != null)
                {
                    return _header_LoaiChiPhi;
                }

                _header_LoaiChiPhi = new HeaderTextFilterModel(Constant.ColumnName_LoaiChiPhi);
                return _header_LoaiChiPhi;
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

        private static ChiPhiViewModel _viewModel;
        public static ChiPhiViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiPhiViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tChiPhi Create(int i)
        {
            return new tChiPhi()
            {
                Ma = i,
                MaLoaiChiPhi = i,
                MaNhanVienGiaoHang = i,
                SoTien = i * 1000,
                GhiChu = "Ghi chú " + i,
                rNhanVien = DDrNhanVien.Create(i),
                rLoaiChiPhi = DDrLoaiChiPhi.Create(i),
                rNhanVienList = DDrNhanVien.ViewModel.Entity.ToList(),
                rLoaiChiPhiList = DDrLoaiChiPhi.ViewModel.Entity.ToList()
            };
        }
    }
}
