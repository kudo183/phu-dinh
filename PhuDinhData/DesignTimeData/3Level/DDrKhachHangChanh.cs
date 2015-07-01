using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDrKhachHangChanh
    {
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

        private static HeaderTextFilterModel _header_Chanh;
        public static HeaderTextFilterModel Header_Chanh
        {
            get
            {
                if (_header_Chanh != null)
                {
                    return _header_Chanh;
                }

                _header_Chanh = new HeaderTextFilterModel(Constant.ColumnName_Chanh);
                return _header_Chanh;
            }
        }

        private static KhachHangChanhViewModel _viewModel;
        public static KhachHangChanhViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new KhachHangChanhViewModel();

                const int count = 10;

                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static rKhachHangChanh Create(int i)
        {
            return new rKhachHangChanh()
            {
                Ma = i,
                MaChanh = i,
                MaKhachHang = i,
                LaMacDinh = i % 2 == 0,
                rKhachHang = DDrKhachHang.Create(i),
                rChanh = DDrChanh.Create(i),
                rKhachHangList = DDrKhachHang.ViewModel.Entity.ToList(),
                rChanhList = DDrChanh.ViewModel.Entity.ToList()
            };
        }
    }
}
