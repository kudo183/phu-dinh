using PhuDinhDataEntity;
using System.Linq;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public class DDrKhachHangChanh
    {

        private static KhachHangChanhViewModel _viewModel;
        public static KhachHangChanhViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new KhachHangChanhViewModel();

                const int count = 10;

                for (int i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));

                }

                return _viewModel;
            }
        }

        public static rKhachHangChanh Create(int i)
        {
            return new rKhachHangChanh()
            {
                Ma = i,
                MaChanh = i,
                MaKhachHang = i,
                LaMacDinh = i % 2 == 0,
                rKhachHang = DDrKhachHang.Create(i),
                rChanh = DDrChanh.Create(i),
                rKhachHangList = DDrKhachHang.ViewModel.Entity.ToList(),
                rChanhList = DDrChanh.ViewModel.Entity.ToList()
            };
        }
    }
}
