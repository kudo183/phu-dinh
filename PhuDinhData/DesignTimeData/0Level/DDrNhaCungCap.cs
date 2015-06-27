using PhuDinhDataEntity;
using PhuDinhData.ViewModel;

namespace PhuDinhData.DesignTimeData
{
    public static class DDrNhaCungCap
    {
        private static NhaCungCapViewModel _viewModel;
        public static NhaCungCapViewModel ViewModel
        {
            get
            {
                if (_viewModel != null)
                {
                    return _viewModel;
                }

                _viewModel = new NhaCungCapViewModel();

                const int count = 10;

                for (var i = 1; i <= count; i++)
                {
                    _viewModel.Entity.Add(Create(i));
                }

                return _viewModel;
            }
        }

        public static rNhaCungCap Create(int i)
        {
            return new rNhaCungCap()
            {
                Ma = i,
                TenNhaCungCap = "Nha cung cap " + i
            };
        }
    }
}
