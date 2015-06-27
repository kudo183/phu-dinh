using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtChiTietNhapHang
    {
        private static ChiTietNhapHangViewModel _viewModel;
        public static ChiTietNhapHangViewModel ViewModel
        {
            get 
            {
                if(_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new ChiTietNhapHangViewModel();

                const int count = 10;
                
                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static tChiTietNhapHang Create(int i)
        {
            return new tChiTietNhapHang()
            {
                Ma = i,
                MaNhapHang = i,
                MaMatHang = i,
                SoLuong = i,
                tNhapHang = DDtNhapHang.Create(i),
                tNhapHangList = DDtNhapHang.ViewModel.Entity.ToList(),
                tMatHang = DDtMatHang.Create(i),
                tMatHangList = DDtMatHang.ViewModel.Entity.ToList()
            };
        }
    }
}
