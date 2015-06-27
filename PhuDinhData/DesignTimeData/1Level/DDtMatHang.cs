using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDtMatHang
    {
        private static MatHangViewModel _viewModel;
        public static MatHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new MatHangViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static tMatHang Create(int i)
        {
            return new tMatHang()
            {
                Ma = i,
                MaLoai = i,
                TenMatHang = "Mặt hàng " + i,
                TenMatHangDayDu = "Mặt hàng đầy đủ " + i,
                TenMatHangIn = "Mặt hàng In " + i,
                SoKy = i,
                SoMet = i * 10,
                rLoaiHang = DDrLoaiHang.Create(i),
                rLoaiHangList = DDrLoaiHang.ViewModel.Entity.ToList()
            };
        }
    }
}
