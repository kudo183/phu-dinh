using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtChiTietChuyenHangDonHang
    {
        private static ChiTietChuyenHangDonHangViewModel _viewModel;
        public static ChiTietChuyenHangDonHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiTietChuyenHangDonHangViewModel();

                const int count = 10;

                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tChiTietChuyenHangDonHang Create(int i)
        {
            return new tChiTietChuyenHangDonHang()
            {
                Ma = i,
                MaChiTietDonHang = i,
                MaChuyenHangDonHang = i,
                SoLuong = i * 10,
                tChiTietDonHang = DDtChiTietDonHang.Create(i),
                tChuyenHangDonHang = DDtChuyenHangDonHang.Create(i),
                tChiTietDonHangList = DDtChiTietDonHang.tChiTietDonHangs,
                tChuyenHangDonHangList = DDtChuyenHangDonHang.ViewModel.Entity.ToList()
            };
        }
    }
}
