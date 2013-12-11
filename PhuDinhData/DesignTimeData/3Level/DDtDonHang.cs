using System.Collections.ObjectModel;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDtDonHang
    {
        private static DGDonHangViewModel _viewModel;
        public static DGDonHangViewModel ViewModel
        {
            get 
            {
                if(_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new DGDonHangViewModel();

                const int count = 10;
                var donhangs = new ObservableCollection<tDonHang>();
                for (int i = 1; i <= count; i++)
                {
                    donhangs.Add(Create(i));

                }

                _viewModel.Entity = donhangs;

                return _viewModel;
            }
        }

        public static tDonHang Create(int i)
        {
            return new tDonHang()
            {
                Ma = i,
                MaChanh = i,
                MaKhachHang = i,
                Ngay = System.DateTime.Now,
                rKhachHang = DDrKhachHang.Create(i),
                rChanh = DDrChanh.Create(i),
                rKhachHangList = DDrKhachHang.rKhachHangs,
                rChanhList = DDrChanh.ViewModel.Entity.ToList()
            };
        }
    }
}
