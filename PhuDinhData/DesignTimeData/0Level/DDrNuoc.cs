using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNuoc
    {
        private static NuocViewModel _viewModel;
        public static NuocViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NuocViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rNuoc Create(int i)
        {
            return new rNuoc()
            {
                Ma = i,
                TenNuoc = "Nước " + i
            };
        }
    }
}
