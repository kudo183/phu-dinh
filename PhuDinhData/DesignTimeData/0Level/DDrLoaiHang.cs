using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrLoaiHang
    {
        private static LoaiHangViewModel _viewModel;
        public static LoaiHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new LoaiHangViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rLoaiHang Create(int i)
        {
            return new rLoaiHang()
            {
                Ma = i,
                TenLoai = "Loại hàng " + i
            };
        }
    }
}
