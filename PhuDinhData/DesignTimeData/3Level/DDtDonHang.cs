using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtDonHang
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

        private static HeaderTextFilterModel _header_KhachHang;
        public static HeaderTextFilterModel Header_KhachHang
        {
            get
            {
                if (_header_KhachHang != null)
                {
                    return _header_KhachHang;
                }

                _header_KhachHang = new HeaderTextFilterModel(Constant.ColumnName_KhachHang);
                return _header_KhachHang;
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

        private static HeaderTextFilterModel _header_KhachHangChanh;
        public static HeaderTextFilterModel Header_KhachHangChanh
        {
            get
            {
                if (_header_KhachHangChanh != null)
                {
                    return _header_KhachHangChanh;
                }

                _header_KhachHangChanh = new HeaderTextFilterModel(Constant.ColumnName_KhachHangChanh);
                return _header_KhachHangChanh;
            }
        }

        private static DonHangViewModel _viewModel;
        public static DonHangViewModel ViewModel
        {
            get 
            {
                if(_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new DonHangViewModel();

                const int count = 10;
                
                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tDonHang Create(int i)
        {
            return new tDonHang()
            {
                Ma = i,
                MaChanh = i,
                MaKhachHang = i,
                MaKhoHang = i,
                Ngay = System.DateTime.Now,
                rKhachHang = DDrKhachHang.Create(i),
                rKhachHangList = DDrKhachHang.ViewModel.Entity.ToList(),
                rChanh = DDrChanh.Create(i),
                rChanhList = DDrChanh.ViewModel.Entity.ToList(),
                rKhoHang = DDrKhoHang.Create(i),
                rKhoHangList = DDrKhoHang.ViewModel.Entity.ToList()
            };
        }
    }
}
