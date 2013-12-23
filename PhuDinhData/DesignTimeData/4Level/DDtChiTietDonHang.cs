using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiTietDonHang
    {
        private static ChiTietDonHangViewModel _viewModel;
        public static ChiTietDonHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiTietDonHangViewModel();

                const int count = 10;

                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tChiTietDonHang Create(int i)
        {
            return new tChiTietDonHang()
            {
                Ma = i,
                MaDonHang = i,
                MaMatHang = i,
                SoLuong = i * 10,
                tDonHang = DDtDonHang.Create(i),
                tMatHang = DDtMatHang.Create(i),
                tDonHangList = DDtDonHang.ViewModel.Entity.ToList(),
                tMatHangList = DDtMatHang.ViewModel.Entity.ToList()
            };
        }
    }
}
