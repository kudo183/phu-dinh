using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrKhoHang
    {
        private static KhoHangViewModel _viewModel;
        public static KhoHangViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new KhoHangViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rKhoHang Create(int i)
        {
            return new rKhoHang()
            {
                Ma = i,
                TenKho = "Kho " + i,
                TrangThai = (i & 1) == 0
            };
        }
    }
}
