using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtChuyenHangDonHang
    {
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
                tChuyenHangList = DDtChuyenHang.tChuyenHangs,
                tDonHangList = DDtDonHang.ViewModel.Entity.ToList()
            };
        }
    }
}
