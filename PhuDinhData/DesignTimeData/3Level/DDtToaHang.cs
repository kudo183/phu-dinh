using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtToaHang
    {
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
