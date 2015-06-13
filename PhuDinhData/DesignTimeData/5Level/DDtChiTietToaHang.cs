using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiTietToaHang
    {
        private static ChiTietToaHangViewModel _viewModel;
        public static ChiTietToaHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiTietToaHangViewModel();

                const int count = 10;

                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tChiTietToaHang Create(int i)
        {
            return new tChiTietToaHang()
            {
                Ma = i,
                MaChiTietDonHang = i,
                MaToaHang= i,
                tChiTietDonHang = DDtChiTietDonHang.Create(i),
                tToaHang = DDtToaHang.Create(i),
                tChiTietDonHangList = DDtChiTietDonHang.ViewModel.Entity.ToList(),
                tToaHangList = DDtToaHang.ViewModel.Entity.ToList()
            };
        }
    }
}
