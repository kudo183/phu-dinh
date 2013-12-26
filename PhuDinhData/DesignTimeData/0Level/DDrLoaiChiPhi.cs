using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrLoaiChiPhi
    {
        private static LoaiChiPhiViewModel _viewModel;
        public static LoaiChiPhiViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new LoaiChiPhiViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rLoaiChiPhi Create(int i)
        {
            return new rLoaiChiPhi()
            {
                Ma = i,
                TenLoaiChiPhi = "Loại chi phí " + i
            };
        }
    }
}
