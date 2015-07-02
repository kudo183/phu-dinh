using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrKhachHang
    {
        private static HeaderTextFilterModel _header_DiaDiem;
        public static HeaderTextFilterModel Header_DiaDiem
        {
            get
            {
                if (_header_DiaDiem != null)
                {
                    return _header_DiaDiem;
                }

                _header_DiaDiem = new HeaderTextFilterModel(Constant.ColumnName_DiaDiem);
                return _header_DiaDiem;
            }
        }

        private static KhachHangViewModel _viewModel;
        public static KhachHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new KhachHangViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rKhachHang Create(int i)
        {
            return new rKhachHang()
            {
                Ma = i,
                MaDiaDiem = i,
                TenKhachHang = "Khách hàng " + i,
                rDiaDiem = DDrDiaDiem.Create(i),
                rDiaDiemList = DDrDiaDiem.ViewModel.Entity.ToList()
            };
        }
    }
}
