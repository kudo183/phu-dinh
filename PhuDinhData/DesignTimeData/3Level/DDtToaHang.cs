using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtToaHang
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

        private static ToaHangViewModel _viewModel;
        public static ToaHangViewModel ViewModel
        {
            get 
            {
                if(_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ToaHangViewModel();

                const int count = 10;
                
                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tToaHang Create(int i)
        {
            return new tToaHang()
            {
                Ma = i,
                MaKhachHang = i,
                Ngay = System.DateTime.Now,
                rKhachHang = DDrKhachHang.Create(i),
                rKhachHangList = DDrKhachHang.ViewModel.Entity.ToList()
            };
        }
    }
}
