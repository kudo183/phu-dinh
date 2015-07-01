using CustomControl.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;
using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtChuyenHangDonHang
    {
        private static HeaderTextFilterModel _header_DonHang;
        public static HeaderTextFilterModel Header_DonHang
        {
            get
            {
                if (_header_DonHang != null)
                {
                    return _header_DonHang;
                }

                _header_DonHang = new HeaderTextFilterModel(Constant.ColumnName_DonHang);
                return _header_DonHang;
            }
        }

        private static ChuyenHangDonHangViewModel _viewModel;
        public static ChuyenHangDonHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChuyenHangDonHangViewModel();

                const int count = 10;

                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tChuyenHangDonHang Create(int i)
        {
            return new tChuyenHangDonHang()
            {
                Ma = i,
                MaChuyenHang = i,
                MaDonHang = i,
                tChuyenHang = DDtChuyenHang.Create(i),
                tDonHang = DDtDonHang.Create(i),
                tChuyenHangList = DDtChuyenHang.ViewModel.Entity.ToList(),
                tDonHangList = DDtDonHang.ViewModel.Entity.ToList()
            };
        }
    }
}
